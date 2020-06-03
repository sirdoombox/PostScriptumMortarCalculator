using System;
using System.Collections.Generic;
using System.Windows;
using PostScriptumMortarCalculator.Events;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.Utils;
using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class CalculatorViewModel : Screen, IHandle<PositionChangedEvent>
    {
        public RoundedVector2 MortarPositionMeters { get; private set; }

        public MapCoordinate MortarPositionCoords { get; set; }
        public RoundedVector2 TargetPositionMeters { get; private set; }

        public MapCoordinate TargetPositionCoords { get; set; }
        
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
        
        private readonly ConfigService m_configService;
        private readonly IEventAggregator m_eventAggregator;

        private double m_mapBounds;
        
        public CalculatorViewModel(MortarDataModel defaultMortar,
            IReadOnlyList<MortarDataModel> mortars,
            ConfigService configService,
            IEventAggregator eventAggregator)
        {
            m_configService = configService;
            m_eventAggregator = eventAggregator;
            m_eventAggregator.Subscribe(this);
            AvailableMortars.AddRange(mortars);
            SelectedMortar = defaultMortar;
        }

        public void OnSelectedMortarChanged()
        {
            m_eventAggregator.Publish(new MortarChangedEvent(SelectedMortar));
            m_configService.ActiveConfig.LastMortarName = SelectedMortar.Name;
            m_configService.SerialiseUserConfig();
        }
        
        
        public void Handle(PositionChangedEvent message)
        {
            MortarPositionMeters = message.MortarPositionMeters;
            TargetPositionMeters = message.TargetPositionMeters;
            MortarPositionCoords = MortarPositionMeters.MetersPositionToMapCoordinate();
            TargetPositionCoords = TargetPositionMeters.MetersPositionToMapCoordinate();
            Angle = message.Angle;
            m_mapBounds = message.MapBounds;
        }

        public void CopyToClipboardClicked()
        {
            Clipboard.SetData(DataFormats.Text, $"Angle: {Angle} | Mills: {Milliradians}");
        }
    }
}