using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using Crypto.Core.Common;
using DevExpress.XtraEditors;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.ComponentModels;
using MOEA.Core.ProblemModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.UI.Strategies {
    public class StrategyOptimizationManager : IMOOProblem {
        public StrategyOptimizationManager(StrategyBase strategy) {
            Strategy = strategy;
            DataProvider = new SimulationStrategyDataProvider();
            Manager = new StrategiesManager(DataProvider);
            Manager.Strategies.Add(Strategy);

            Strategy.Manager = Manager;
            Strategy.OptimizationMode = true;
            Strategy.OptimizationParametersInitialized = false;
            Strategy.InitializeParametersToOptimize();
            Strategy.DemoMode = true;
        }

        protected SimulationStrategyDataProvider DataProvider { get; private set; }
        public StrategiesManager Manager { get; private set; }
        public StrategyBase Strategy { get; private set; }
        public string State { get; set; }
        public string LastError { get; set; }
        public int CurrentIteration { get; set; }
        public double SimulationProgress { get; set; }

        public void ApplyParams(StrategyBase strategy, object[] currentParams) {
            for(int i = 0; i < currentParams.Length; i++) {
                strategy.ParametersToOptimize[i].CurrentValueCore = currentParams[i];
                strategy.ParametersToOptimize[i].ApplyValue();
            }
        }

        public object Calculate(object[] currentParams) {
            StrategyBase strategy = Strategy.Clone();

            strategy.Manager = Manager;
            strategy.OptimizationMode = true;
            strategy.DemoMode = true;
            strategy.InitializeParametersToOptimize();

            Manager.Strategies.Clear();
            Manager.Strategies.Add(strategy);

            ApplyParams(strategy, currentParams);

            if(!Manager.Initialize(DataProvider)) {
                LastError = "Error cannot initialize strategies manager";
                RaiseError();
                return 0;
            }

            if(!Manager.Start()) {
                LastError = "Error starting optimization! Please check log messages";
                RaiseError();
                return 0;
            }

            CurrentIteration++;
            State = string.Format("<b>Running optimization iteration {0} ...</b>", CurrentIteration);
            SimulationProgress = 0;
            RaiseStateChanged();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            int elapsedSeconds = 0;
            double progress = 0;
            while(Manager.Running) {
                SimulationProgress = DataProvider.SimulationProgress;
                //if(timer.ElapsedMilliseconds / 1000 > elapsedSeconds) {
                //    elapsedSeconds = (int)(timer.ElapsedMilliseconds / 1000);
                //    State = string.Format("<b>Running optimization iteration {0}... {1} sec</b>", CurrentIteration, elapsedSeconds);
                //    RaiseStateChanged();
                //}
                if((DataProvider.SimulationProgress - progress) >= 0.05) {
                    elapsedSeconds = (int)(timer.ElapsedMilliseconds / 1000);
                    State = string.Format("<b>Running optimization iteration {0}... {1} sec</b>", CurrentIteration, elapsedSeconds);
                    progress = DataProvider.SimulationProgress;
                    RaiseStateChanged();
                }
            }

            strategy.OutputParameter.GetValue();
            LogManager.Default.Add("GenerationIndex = " + CurrentIteration + " With Params {{" + ParametersToString(strategy) + "}} => " + ((double)strategy.OutputParameter.CurrentValueCore).ToString("0.00000000"));
            return (double)strategy.OutputParameter.CurrentValueCore;
        }

        double IMOOProblem.CalcObjective(MOOSolution s, int objective_index) {
            ContinuousVector x = (ContinuousVector)s;
            object[] currentParams = new object[x.Length];
            for(int i = 0; i < x.Length; i++)
                currentParams[i] = x[i];
            return (double)Calculate(currentParams);
        }

        private string ParametersToString(StrategyBase strategy) {
            StringBuilder b = new StringBuilder();
            for(int i = 0; i < strategy.ParametersToOptimize.Count; i++) {
                b.Append(strategy.ParametersToOptimize[i].CurrentValueCore.ToString());
                if(i < strategy.ParametersToOptimize.Count - 1)
                    b.Append(',');
            }
            return b.ToString();
        }

        private void RaiseError() {
            if(Error != null)
                Error(this, EventArgs.Empty);
        }

        private void RaiseStateChanged() {
            if(StateChanged != null)
                StateChanged(this, EventArgs.Empty);
        }

        public event EventHandler Error;
        public event EventHandler StateChanged;

        int IMOOProblem.GetDimensionCount() {
            return Strategy.ParametersToOptimize.Count;
        }

        double IMOOProblem.GetLowerBound(int dimension_index) {
            return Convert.ToDouble(GetLowerBound(dimension_index));
        }

        int IMOOProblem.GetObjectiveCount() {
            return 1;
        }

        public object GetUpperBound(int index) {
            object value = Strategy.ParametersToOptimize[index].MaxValueCore;
            if(value == null)
                return int.MaxValue;
            return value;
        }

        public object GetLowerBound(int index) {
            object value = Strategy.ParametersToOptimize[index].MinValueCore;
            if(value == null)
                return 0;
            return value;
        }

        public object GetChange(int index) {
            object value = Strategy.ParametersToOptimize[index].ChangeCore;
            if(value == null)
                return 0;
            return value;
        }

        double IMOOProblem.GetUpperBound(int dimension_index) {
            return Convert.ToDouble(GetUpperBound(dimension_index));
        }

        bool IMOOProblem.IsFeasible(MOOSolution s) {
            return true;
        }

        bool IMOOProblem.IsMaximizing() {
            return Strategy.OutputParameter.Optimization == Core.Strategies.Base.OutputParameterOptimizationMode.Maximize;
        }
    }

    public class SimpleOptimizationAlgorithm {
        public SimpleOptimizationAlgorithm(StrategyOptimizationManager manager) {
            Manager = manager;
        }
        protected object[] CurrentValues { get; private set; }
        protected object[] StartValues { get; private set; }
        protected object[] EndValues { get; private set; }
        protected object[] Changes { get; private set; }
        protected object[] OptimalValues { get; private set; }
        protected object Result { get; private set; }
        protected object OptimalResult { get; private set; }
        public StrategyOptimizationManager Manager { get; private set; }
        public void Initialize() {
            int count = Manager.Strategy.ParametersToOptimize.Count;
            var par = Manager.Strategy.ParametersToOptimize;
            CurrentValues = new object[count];
            StartValues = new object[count];
            EndValues = new object[count];
            Changes = new object[count];
            OptimalValues = new object[count];
            for(int i = 0; i < count; i++) {
                CurrentValues[i] = par[i].MinValueCore;
                StartValues[i] = par[i].MinValueCore;
                EndValues[i] = par[i].MaxValueCore;
                Changes[i] = par[i].ChangeCore;
            }
        }
        public bool IsTerminated { get; set; }
        public void Evolve() {
            if(Result != null) {
                if(!NextStep()) {
                    IsTerminated = true;
                    return;
                }
            }
            Result = Calculate();
            if(OptimalResult == null || 
                Compare(Result, OptimalResult, Manager.Strategy.OutputParameter.Optimization == Core.Strategies.Base.OutputParameterOptimizationMode.Maximize)) {
                OptimalResult = Result;
                CurrentValues.CopyTo(OptimalValues, 0);
            }
        }

        private bool Compare(object op1, object op2, bool isGreater) {
            if(isGreater) {
                if(op1 is int) return ((int)op1) >= ((int)op2);
                if(op1 is int) return ((long)op1) >= ((long)op2);
                if(op1 is float) return ((float)op1) >= ((float)op2);
                if(op1 is double) return ((double)op1) >= ((double)op2);
            }
            else {
                if(op1 is int) return ((int)op1) <= ((int)op2);
                if(op1 is int) return ((long)op1) <= ((long)op2);
                if(op1 is float) return ((float)op1) <= ((float)op2);
                if(op1 is double) return ((double)op1) <= ((double)op2);
            }
            return false;
        }

        private object Add(object op1, object op2) {
            if(op1 is int) return ((int)op1) + ((int)op2);
            if(op1 is long) return ((long)op1) + ((long)op2);
            if(op1 is float) return ((float)op1) + ((float)op2);
            if(op1 is double) return ((double)op1) + ((double)op2);
            return op1;
        }

        private bool NextStep() {
            for(int index = 0; index < CurrentValues.Length; index++) {
                CurrentValues[index] = Add(CurrentValues[index], Changes[index]);
                if(!Compare(CurrentValues[index], EndValues[index], true))
                    return true;
                CurrentValues[index] = StartValues[index];
            }
            return false;
        }
        private object Calculate() {
            return Manager.Calculate(CurrentValues);
        }
    }
}
