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

        public double Milliradians =>
            SelectedMortar.GetMilliradiansForDistance(Distance);
        
        private readonly ConfigService m_configService;
        private readonly IEventAggregator m_eventAggregator;
        
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
        }

        public void CopyToClipboardClicked()
        {
            Clipboard.SetData(DataFormats.Text, $"Angle: {Angle} | Mills: {Milliradians}");
        }
    }
}