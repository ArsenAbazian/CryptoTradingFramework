using Crypto.Core.Helpers;
using Crypto.Core.Strategies;
using CryptoMarketClient.Common;
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

        double IMOOProblem.CalcObjective(MOOSolution s, int objective_index) {
            StrategyBase strategy = Strategy.Clone();

            strategy.Manager = Manager;
            strategy.OptimizationMode = true;
            strategy.DemoMode = true;
            strategy.InitializeParametersToOptimize();

            Manager.Strategies.Clear();
            Manager.Strategies.Add(strategy);

            ContinuousVector x = (ContinuousVector)s;
            for(int i = 0; i < x.Length; i++) {
                strategy.ParametersToOptimize[i].CurrentValueCore = x[i];
                strategy.ParametersToOptimize[i].ApplyValue();
            }
            
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
            LogManager.Default.Add("GenerationIndex = " + CurrentIteration + " With Params {{" + ParametersToString(strategy) +"}} => " + ((double)strategy.OutputParameter.CurrentValueCore).ToString("0.00000000"));
            return (double)strategy.OutputParameter.CurrentValueCore;
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
        public event EventHandler Started;
        public event EventHandler StateChanged;

        int IMOOProblem.GetDimensionCount() {
            return Strategy.ParametersToOptimize.Count;
        }

        double IMOOProblem.GetLowerBound(int dimension_index) {
            object value = Strategy.ParametersToOptimize[dimension_index].MinValueCore;
            if(value == null)
                return 0;
            return Convert.ToDouble(value);
        }

        int IMOOProblem.GetObjectiveCount() {
            return 1;
        }

        double IMOOProblem.GetUpperBound(int dimension_index) {
            object value = Strategy.ParametersToOptimize[dimension_index].MaxValueCore;
            if(value == null)
                return int.MaxValue;
            return Convert.ToDouble(value);
        }

        bool IMOOProblem.IsFeasible(MOOSolution s) {
            return true;
        }

        bool IMOOProblem.IsMaximizing() {
            return Strategy.OutputParameter.Optimization == Core.Strategies.Base.OutputParameterOptimizationMode.Maximize;
        }
    }
}
