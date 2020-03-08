using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PostScriptumMortarCalculator.Events;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Utils;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class CalculatorViewModel : Screen, IHandle<PositionChangedEvent>
    {
        #region Properties

        public RoundedVector2 MortarPositionMeters { get; private set; }
        
        public RoundedVector2 TargetPositionMeters { get; private set; }
        
        public BindableCollection<MortarDataModel> AvailableMortars { get; set; } = 
            new BindableCollection<MortarDataModel>();
        
        public MortarDataModel SelectedMortar { get; set; }

        public double Angle { get; private set; }
        
        public double Distance =>
            RoundedVector2.Distance(MortarPositionMeters, TargetPositionMeters);
        
        public double Milliradians
        {
            get
            {
                for (var i = 0; i < SelectedMortar.RangeValues.Count-1; i++)
                {
                    var curr = SelectedMortar.RangeValues[i];
                    var next = SelectedMortar.RangeValues[i + 1];
                    if (Distance < curr.Distance || Distance > next.Distance) continue;
                    var perc = Distance.PercentageBetween(curr.Distance, next.Distance);
                    return Math.Round(perc.LerpBetween(curr.Milliradians, next.Milliradians), 2);
                }
                return 0;
            }
        }

        #endregion

        #region Private Members

        private readonly IEventAggregator m_eventAggregator;

        #endregion

        #region Initialisation

        public CalculatorViewModel(
            IReadOnlyList<MortarDataModel> mortars, 
            IEventAggregator eventAggregator)
        {
            m_eventAggregator = eventAggregator;
            m_eventAggregator.Subscribe(this);
            AvailableMortars.AddRange(mortars);
            SelectedMortar = AvailableMortars.First();
            
        }

        protected override void OnViewLoaded()
        {
            m_eventAggregator.Publish(new MortarChangedEvent(SelectedMortar));
        }

        #endregion

        #region PropertyChanged Handlers

        public void OnSelectedMortarChanged()
        {
            m_eventAggregator.Publish(new MortarChangedEvent(SelectedMortar));
        }
        
        
        public void Handle(PositionChangedEvent message)
        {
            MortarPositionMeters = message.MortarPositionMeters;
            TargetPositionMeters = message.TargetPositionMeters;
            Angle = message.Angle;
        }

        #endregion

        #region UI Event Handlers

        public void CopyToClipboardClicked()
        {
            Clipboard.SetData(DataFormats.Text, $"Angle: {Angle} | Mills: {Milliradians}");
        }

        #endregion

    }
}