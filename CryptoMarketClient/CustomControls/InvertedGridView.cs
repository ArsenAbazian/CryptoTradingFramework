using DevExpress.Utils;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public class InvertedGridView : GridView {
        protected override void CreateViewInfo() {
            this.fViewInfo = new InvertedGridViewInfo(this);
        }
        protected internal bool UseDirectXPaintCore { get { return UseDirectXPaint; } }
        protected internal bool IsPixelScrollingCore { get { return IsPixelScrolling;} }
        System.Reflection.MethodInfo miReset;
        protected internal void ResetAllowUpdateRowIndexesCore() { 
            if(this.miReset == null)
                this.miReset = GetType().GetMethod("ResetAllowUpdateRowIndexes", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            this.miReset.Invoke(this, null);
        }
        protected override ScrollInfo CreateScrollInfo() {
            return new InvertedScrollInfo(this);
        }
        protected override GridViewScroller CreateGridScroller() {
            return new InvertedGridViewByPixelScroller(this);
        }
        protected internal GridViewScroller ScrollerCore => Scroller;
        protected internal DirectXScroller DirectXScrollerCore => DirectXScroller;
    }

    public class InvertedGridViewByPixelScroller : GridViewByPixelScroller {
        public InvertedGridViewByPixelScroller(GridView view) : base(view) { }
        protected override ScrollPositionInfo CreateScrollInfo() {
            return new InvertedPixelScrollPositionInfo();
        }
        protected override void OnMouseWheelScroll(MouseWheelScrollClientArgs e) {
            //if(View.IsInplaceEditFormVisible) return; TODO!!!
            int height = ScrollInfo.GetScrollableBounds(ViewInfo).Height;
            int wheelDistance = 0;
            bool useSmoothScroll = true;
            if (!e.InPixels) {
                wheelDistance = (e.Distance < 0 ? -1 : 1) * Math.Min(height, Math.Max(300, height / 3));
            }
            else {
                wheelDistance = e.Distance;
                useSmoothScroll = false;
            }
            if(wheelDistance + View.TopRowPixel + height > ViewInfo.VisibleRowsHeight) wheelDistance = Math.Max(0, ViewInfo.VisibleRowsHeight - View.TopRowPixel - height);
            if(IView.UseDirectXPaintCore) {
                if(!useSmoothScroll) CancelSmoothScroll();
                int offset = wheelDistance;
                Rectangle scrollRect = IView.ScrollerCore.ScrollInfo.GetScrollableBounds(ViewInfo);
                Rectangle invalidateRect = ViewInfo.ViewRects.Rows;
                ViewInfo.SelectionInfo.OnHotTrackLeave();
                IView.DirectXScrollerCore.RunAnimation(invalidateRect, scrollRect, offset);
                return;
            }
            if(useSmoothScroll) {
                SmoothScroll(wheelDistance, false);
            }
            else {
                CancelSmoothScroll();
                View.TopRowPixel += wheelDistance;
            }
        }
        protected InvertedGridView IView { get { return (InvertedGridView)View; } }
    }

    public class InvertedPixelScrollPositionInfo : PixelScrollPositionInfo {
        public InvertedPixelScrollPositionInfo() : base() {

        }
        int topPixelPosition = -1, lastTopPixelPosition = -1;
        Rectangle scrollableBounds = Rectangle.Empty;
        public override bool IsEmpty { get { return topPixelPosition < 0; } }
        public override void Clear() {
            base.Clear();
            this.topPixelPosition = -1;
        }
        public override void Save(GridViewInfo viewInfo) {
            base.Save(viewInfo);
            this.lastTopPixelPosition = this.topPixelPosition = viewInfo.View.TopRowPixel;
            this.scrollableBounds = GetScrollableBounds(viewInfo);
        }
        public override Rectangle GetScrollableBounds(GridViewInfo viewInfo) {
            Rectangle res = viewInfo.ViewRects.Rows;
            GridRowInfo row = viewInfo.RowsInfo.GetLastNonScrollableRow(viewInfo.View, false);
            int rowBottomBounds = row == null ? res.Y : row.TotalBounds.Bottom;
            //if(viewInfo.View.AllowFixedGroups) { TODO!!!
            //    int index = row == null ? 0 : viewInfo.RowsInfo.IndexOf(row) + 1;
            //    while(index < viewInfo.RowsInfo.Count) {
            //        GridRowInfo checkRow = viewInfo.RowsInfo[index];
            //        if(checkRow.IsGroupRow && checkRow.IsGroupRowExpanded) {
            //            row = checkRow;
            //            index++;
            //            continue;
            //        }
            //        break;
            //    }
            //    if(row != null) rowBottomBounds = Math.Max(rowBottomBounds, row.TotalBounds.Bottom);
            //}

            if(row != null && rowBottomBounds > res.Y) {
                res.Y = rowBottomBounds;
                res.Height = viewInfo.ViewRects.Rows.Bottom - res.Y;
            }
            return res;
        }
        public override Rectangle GetTotalRowsScrollableBounds(GridViewInfo viewInfo) {
            Rectangle res = viewInfo.ViewRects.Rows;
            GridRowInfo row = viewInfo.RowsInfo.GetLastNonScrollableRow(viewInfo.View, false);
            if(row != null) {
                res.Y = row.TotalBounds.Bottom;
                res.Height = viewInfo.ViewRects.Rows.Bottom - res.Y;
            }
            return res;
        }
        public override Rectangle GetCurrentScrollableBounds(GridViewInfo viewInfo) {
            return scrollableBounds;
        }
        //int invalidateCounter = 0;
        public override void InvalidateNonScrollableArea(GridViewInfo viewInfo) {
            //if(!viewInfo.View.AllowFixedGroups) return;
            Rectangle rect = GetScrollableBounds(viewInfo);
            Rectangle prev = scrollableBounds;
            //if(rect == prev) return;
            Rectangle inv = viewInfo.ViewRects.Rows;
            inv.Height = Math.Max(prev.Top, rect.Top) - inv.Top;
            if(inv.Height > 0) viewInfo.View.InvalidateRect(inv);
            //System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", invalidateCounter++, inv));
        }

        public override Point GetDistance(GridViewInfo viewInfo) { //x = distance, y == y
            Point res = Point.Empty;
            if(IsEmpty) return res;
            Rectangle rect = GetCurrentScrollableBounds(viewInfo);
            res.X = -(this.topPixelPosition - viewInfo.View.TopRowPixel);
            if(Math.Abs(res.X) > 0) {
                res.Y = rect.Y;
                if(res.X < 0)
                    res.Y = rect.Y + Math.Abs(res.X);
            }
            return res;
        }
    }

    public class InvertedScrollInfo : ScrollInfo {
        public InvertedScrollInfo(BaseView view) : base(view) { }
        protected override VCrkScrollBar CreateVScroll() {
            VCrkScrollBar res = new InvertedVCrkScrollBar(this);
            ScrollBarBase.ApplyUIMode(res);
            res.RightToLeft = RightToLeft.No;
            return res;
        }
    }

    public class InvertedVCrkScrollBar : VCrkScrollBar, IScrollBar {
        public InvertedVCrkScrollBar(ScrollInfo info) : base(info) { }
        protected override ScrollBarViewInfo CreateScrollBarViewInfo() {
            return new InvertedScrollBarViewInfo(this);
        }
        Type BaseType { get { return typeof(ScrollBarBase); } }
        FieldInfo fiBeforeThumbDraggingValue;
        protected int GetBeforeThumbDraggingValue() {
            if(this.fiBeforeThumbDraggingValue == null)
                this.fiBeforeThumbDraggingValue = BaseType.GetField("beforeThumbDraggingValue", BindingFlags.Instance | BindingFlags.NonPublic);
            return (int)this.fiBeforeThumbDraggingValue.GetValue(this);
        }
        void IScrollBar.ChangeValueBasedByState(ScrollBarState state) {
			int newValue = Value;
			ScrollEventType scrollType = ScrollEventType.EndScroll;
			switch(state) {
				case ScrollBarState.IncButtonPressed:
					newValue -= SmallChange; 
					scrollType = ScrollEventType.SmallIncrement;
					break;
				case ScrollBarState.DecButtonPressed:
					newValue += SmallChange; 
					scrollType = ScrollEventType.SmallDecrement;
					break;
				case ScrollBarState.IncAreaPressed:
					newValue -= LargeChange; 
					scrollType = ScrollEventType.LargeIncrement;
					break;
				case ScrollBarState.DecAreaPressed:
					newValue += LargeChange; 
					scrollType = ScrollEventType.LargeDecrement;
					break;
			}
			if(scrollType != ScrollEventType.EndScroll)
				SetScrollBarValueCore(scrollType, newValue);
		}
        protected override void OnMouseMove(MouseEventArgs e) {
            if(State == ScrollBarState.ThumbPressed) {
				int pos = (ScrollBarType.Horizontal == ScrollBarType) ? e.X: e.Y;
                if(PositionRestoreHelper.ShouldRestore(pos, ViewInfo, DrawMode)) {
                    SetScrollBarValueCore(ScrollEventType.ThumbTrack, GetBeforeThumbDraggingValue());
                } else {
                    int iPos = ((InvertedScrollBarViewInfo)ViewInfo).GetInvertedValueByPos(pos);
                    SetScrollBarValueCore(ScrollEventType.ThumbTrack, iPos);
                    LayoutChanged();
                }
			}
			ViewInfo.UpdateHotState(new Point(e.X, e.Y));
        }
        private void SetScrollBarValueCore(ScrollEventType scrollType, int value) {
            CheckDirectXOwnerCore(scrollType);
			value = GetScrollBarValueCore(value);
			ScrollEventArgs e = new ScrollEventArgs(scrollType, Value, value, ScrollBarType == ScrollBarType.Horizontal ? System.Windows.Forms.ScrollOrientation.HorizontalScroll : System.Windows.Forms.ScrollOrientation.VerticalScroll);
            OnScrollCore(e);
		}
        private int GetScrollBarValueCore(int value) {
			if(value < Minimum) return Minimum;
			if(value > ViewInfo.VisibleMaximum) return ViewInfo.VisibleMaximum;
			return value;
		}
        void CheckDirectXOwnerCore(ScrollEventType scrollType) {
            IDirectXClient dxClient = Parent as IDirectXClient;
            if(dxClient == null || !dxClient.UseDirectXPaint)
                return;
            if(scrollType == ScrollEventType.EndScroll)
                dxClient.DirectXProvider.UnlockRenderToGraphics();
            else dxClient.DirectXProvider.LockRenderToGraphics();
        }
    }

    public class InvertedScrollBarViewInfo : ScrollBarViewInfo { 
        public InvertedScrollBarViewInfo(IScrollBar scrollBar) : base(scrollBar) { }
        protected override Rectangle CalcThumbButtonBounds { 
            get { 
                return CalcThumbButtonBoundsCore();
            }
        }

        Type BaseType { get { return typeof(ScrollBarViewInfo); } }

        FieldInfo fiThumbTrackValueInfo;
        protected float GetThumbTrackValue() {
            if(this.fiThumbTrackValueInfo == null)
                this.fiThumbTrackValueInfo = BaseType.GetField("thumbTrackValue", BindingFlags.Instance | BindingFlags.NonPublic);
            return (float)this.fiThumbTrackValueInfo.GetValue(this);
        }
        protected void SetThumbTrackValue(float value) {
            if(this.fiThumbTrackValueInfo == null)
                this.fiThumbTrackValueInfo = BaseType.GetField("thumbTrackValue", BindingFlags.Instance | BindingFlags.NonPublic);
            this.fiThumbTrackValueInfo.SetValue(this, value);
        }

        FieldInfo fiOldTrackingPos;
        protected int GetOldTrackingPos() {
            if(this.fiOldTrackingPos == null)
                this.fiOldTrackingPos = BaseType.GetField("oldTrackingPos", BindingFlags.Instance | BindingFlags.NonPublic);
            return (int)this.fiOldTrackingPos.GetValue(this);
        }
        protected void SetOldTrackingPos(int value) {
            if(this.fiOldTrackingPos == null)
                this.fiOldTrackingPos = BaseType.GetField("oldTrackingPos", BindingFlags.Instance | BindingFlags.NonPublic);
            this.fiOldTrackingPos.SetValue(this, value);
        }

        public int GetInvertedValueByPos(int pos) {
            float oldThumbTrackValue = GetThumbTrackValue();
            float newThumbTrackValue = oldThumbTrackValue;
            if(pos < ButtonWidth)
                SetThumbTrackValue((!IsRightToLeft) ? Minimum : VisibleMaximum);
            else {
                if(pos > ScrollBarWidth - ButtonWidth)
                    SetThumbTrackValue((!IsRightToLeft) ? VisibleMaximum : Minimum);
                else {
                    int thumbWidth;
                    if(ScrollBarType == ScrollBarType.Horizontal)
                        thumbWidth = ThumbButtonBounds.Width;
                    else thumbWidth = ThumbButtonBounds.Height;
                    float t = ScrollAreaWidth != thumbWidth ? (float)(ScrollAreaWidth - thumbWidth) : 1f;
                    float dif = -(float)(pos - GetOldTrackingPos()) * (float)(VisibleMaximum - Minimum) / t;
                    if(!IsRightToLeft) {
                        newThumbTrackValue += dif;
                        if(dif > 0 && ThumbTrackIntValue < VisibleValue)
                            newThumbTrackValue = VisibleValue;
                        if(dif < 0 && ThumbTrackIntValue > VisibleValue)
                            newThumbTrackValue = VisibleValue;
                    }
                    else 
                        newThumbTrackValue += dif;
                }
            }
            if(newThumbTrackValue < Minimum)
                newThumbTrackValue = Minimum;
            if(newThumbTrackValue > VisibleMaximum)
                newThumbTrackValue = VisibleMaximum;
            if(oldThumbTrackValue != newThumbTrackValue)
                SetOldTrackingPos(pos);
            SetThumbTrackValue(newThumbTrackValue);
            return ThumbTrackIntValue;
        }

        protected Rectangle CalcThumbButtonBoundsCore() {
            if(!IsThumbVisible) return Rectangle.Empty;
            //int invertedValue = Maximum - ScrollBar.Value;
            Rectangle bounds = CalcThumbBoundsCore(ScrollBarType == DevExpress.XtraEditors.ScrollBarType.Horizontal,
                Minimum, Maximum,
                LargeChange,
                ScrollBar.Value, ScrollAreaWidth,
                ScrollBar.DrawMode == ScrollBarDrawMode.TouchMode ? 30 : 0, ButtonWidth, ButtonCornerSize, IsRightToLeft);
            if(!bounds.IsEmpty) {
                if(ScrollBarType == DevExpress.XtraEditors.ScrollBarType.Horizontal)
                    bounds.Height = ScrollBarHeightCore;
                else
                    bounds.Width = ScrollBarHeightCore;
            }
            return bounds;
        }
        protected Rectangle CalcThumbBoundsCore(bool horz, int minimum, int maximum, int largeChange, int value, int scrollAreaSize, int thumbMinWidth, int buttonWidth, int buttonCornerSize, bool isRightToLeft) {
            if(thumbMinWidth == 0) thumbMinWidth = System.Windows.Forms.SystemInformation.HorizontalScrollBarThumbWidth;
		    int visibleMaximum = Math.Max(maximum - largeChange + 1, 1);
            if(maximum - minimum + 1 == 0) return Rectangle.Empty;
            int thumbWidth = (int)((long)scrollAreaSize * largeChange / (maximum - minimum + 1));
            if(largeChange > 1 &&
               (thumbWidth > scrollAreaSize - scrollAreaSize / largeChange) &&
               largeChange <= maximum)
                thumbWidth = scrollAreaSize - scrollAreaSize / largeChange;
            if(thumbWidth < thumbMinWidth)
                thumbWidth = thumbMinWidth;
            if(thumbWidth > scrollAreaSize) thumbWidth = scrollAreaSize;
            Int64 startPos;
            startPos = (scrollAreaSize - thumbWidth) * (Int64)(value - minimum);
            startPos /= (visibleMaximum - minimum);
            if(!isRightToLeft) {
                startPos = (scrollAreaSize - thumbWidth - startPos) + buttonWidth - buttonCornerSize;
            }
            else {
                startPos = Math.Min(startPos, scrollAreaSize - thumbWidth) + buttonWidth - buttonCornerSize;
            }
            if(horz)
                return new Rectangle((int)startPos, 0, thumbWidth, 0);
            else return new Rectangle(0, (int)startPos, 0, thumbWidth);
        }
        protected int ScrollBarHeightCore {
			get {
				if(ScrollBarType == ScrollBarType.Horizontal)
					return ScrollBar.GetHeight();
				else return ScrollBar.GetWidth();
			}
		}
    }

    public class InvertedGridViewInfo : GridViewInfo {
        public InvertedGridViewInfo(InvertedGridView view) : base(view) {

        }
        protected InvertedGridView IView  { get { return (InvertedGridView)View; } }
        bool IsExternalRowCore(int rowHandle) { return View.IsExternalRow(rowHandle); }
        protected override void CalcRowsDrawInfo() {
            if(AllowUpdateDetails) EditFormBounds = Rectangle.Empty;
            CalcDataRight();
            ViewRects.EmptyRows = Rectangle.Empty;
            int bottom = ViewRects.Rows.Bottom;
            GridRow row = null;
            int visibleCount = RowsLoadInfo.ResultRows.Count;
            bool bottomPositionUpdated = false;
            ViewRects.RowsTotalHeight = 0;
            DevExpress.XtraGrid.Drawing.GridColumnInfoArgs lastColumnInfo = ColumnsInfo.LastColumnInfo;
            GridRowInfo lastRow = null;
            RowsCache.UpdateCache(RowsLoadInfo.ResultRows);
            for(int n = 0; n < visibleCount; n++) {
                row = RowsLoadInfo.ResultRows[n];
                if(!bottomPositionUpdated && IView.IsPixelScrollingCore) {
                    bottomPositionUpdated = CheckUpdateTopPositionCore(row, ref bottom);
                    if(bottomPositionUpdated && lastRow != null && lastRow.ForcedRow) {
                        if(lastRow.TotalBounds.Top < bottom) lastRow.DrawMoreIcons = true;
                    }
                }
                GridRowInfo cached = CheckRowCache(row.RowHandle, row.VisibleIndex, bottom);
                GridRowInfo ri;
                GridRow nextRow = (n + 1 < visibleCount ? RowsLoadInfo.ResultRows[n + 1] : null);
                int nextRowLevel = nextRow == null ? -1 : nextRow.Level;
                int rowLineHeight = -1;
                if(cached == null) {
                    rowLineHeight = CalcRowHeight(GInfo.Graphics, row.RowHandle, row.VisibleIndex, row.Level);
                    cached = CheckRowCacheReusable(row, nextRowLevel, bottom - rowLineHeight, rowLineHeight);
                }
                if(cached != null && row.RowHandle == View.FocusedRowHandle && View.IsEditFormVisible) {
                    cached = null;
                }
                if(cached != null) {
                    RowsCache.RemoveRow(cached);
                    GridDataRowInfo cachedDataRow = cached as GridDataRowInfo;
                    bool allowCachedRow = cachedDataRow == null || cachedDataRow.DetailBounds.IsEmpty;
                    if(cachedDataRow != null && cachedDataRow.EditFormRow) allowCachedRow = false;
                    if(allowCachedRow) {
                        lastRow = ri = cached;
                        bottom = ri.TotalBounds.Top;
                        ri.ForcedRow = row.ForcedRow;
                        ri.ForcedRowLight = row.ForcedRowLight;
                        ri.DrawMoreIcons = !row.NextRowPrimaryChild && (row.ForcedRow || (ri.IsGroupRow && ri.IsGroupRowExpanded));
                        RowsInfo.AddRow(ri);
                        if(bottom < ViewRects.Rows.Top) break;
                        continue;
                    }
                }
                lastRow = ri = CreateRowInfo(row);
                Rectangle rowBounds = ViewRects.Rows;
                rowBounds = ViewRects.Rows;
                rowBounds.Y = bottom;
                ri.RowLineHeight = rowLineHeight > 0 ? rowLineHeight : CalcRowHeight(GInfo.Graphics, ri.RowHandle, ri.VisibleIndex, ri.Level);
                rowBounds.Height = ri.RowLineHeight * GetRowLineCount(ri.RowHandle, ri.IsGroupRow);
                rowBounds.Y -= rowBounds.Height;
                ri.Bounds = rowBounds;
                rowBounds.Height = CalcTotalRowHeight(GInfo.Graphics, ri.RowLineHeight, ri.RowHandle, ri.VisibleIndex, ri.Level, ri.IsGroupRow ? (bool?)null : (bool?)true);
                ri.TotalBounds = rowBounds;
                if(IsExternalRowCore(ri.RowHandle)) ri.Bounds = ri.TotalBounds;
                CalcRowIndents(ri);

                ri.RowFooters.RowFooterCount = GetRowFooterCountEx(ri.RowHandle, ri.VisibleIndex, (ri.IsGroupRow ? (bool?)null : (bool?)true));
                ri.RowFooters.RowFootersHeight = ri.RowFooters.RowFooterCount * GroupFooterHeight;

                CalcDataRow(ri as GridDataRowInfo, row, nextRow);
                CalcGroupRow(ri as GridGroupRowInfo, row, nextRow);
                CalcExternalRow(ri as GridExternalRowInfo, row, nextRow);
                //add rowSeparator
                bottom = ri.TotalBounds.Top;

                RowsInfo.AddRow(ri);
                if(bottom < ViewRects.Rows.Top) break;
            }
            //RemoveAnimatedItems(RowsCache.Rows);
            ViewRects.RowsTotalHeight = ViewRects.Rows.Bottom - bottom;
            if(bottom > ViewRects.Rows.Top) {
                Rectangle r = ViewRects.Rows;
                r.Y = ViewRects.Rows.Top;
                r.Height = bottom - ViewRects.Rows.Top;
                ViewRects.EmptyRows = r;
            }
            //CalcRowsMergeInfo();
            //if(AllowUpdateDetails) {
            //    CheckEditFormVisibility();
            //}
			IView.ResetAllowUpdateRowIndexesCore();
		}
        bool IsExternalRow(int rowHandle) { return View.IsExternalRow(rowHandle); }
        protected override GridRowInfo CheckRowCache(int rowHandle, int visibleIndex, int newY) { //todoCache
            if(!HasRowsCache) return null;
            GridRowInfo res = null;
            if(IsExternalRow(rowHandle)) return null;
            //    res = CachedRows.FindRowByVisibleIndex(visibleIndex);
            //else
            res = RowsCache.FindExistingRow(rowHandle);
            if(res == null) return null;
            if(res.Loaded != View.IsRowLoaded(rowHandle)) return null;
            res.OffsetContent(0, newY - (res.Bounds.Bottom));
            return res;
        }

        private bool CheckUpdateTopPositionCore(GridRow row, ref int bottom) {
            if(row.ForcedRow) return false;
            if(View.IsNewItemRow(row.RowHandle) && View.OptionsView.NewItemRowPosition == NewItemRowPosition.Top) return false;
            if(View.IsFilterRow(row.RowHandle)) return false;
            bottom += View.TopRowPixel - (CalcPixelPositionByRow(View.TopRowIndex)); //todo row.Index?
            return true;
        }

        protected override GridPixelPositionCalculatorBase CreatePositionHelper() {
            return new InvertedGridPixelPositionCalculator(this);
        }
    }

    public class InvertedGridPixelPositionCalculator : GridPixelPositionCalculatorCached {
        public InvertedGridPixelPositionCalculator(GridViewInfo info) : base(info) {

        }
        protected override int CalcTotalsRowHeightNoGroups() {
            return base.CalcTotalsRowHeightNoGroups();
        }
    }
}
