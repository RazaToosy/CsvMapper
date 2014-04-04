using System;
using System.ComponentModel;
using System.IO;
using CsvMapper.Core.Models;
using CsvMapper.Core.Services;
using CsvMapper.Infrastructure.Logging;
using CsvMapper.Infrastructure.ResolvingDepdendencies;
using CsvMapper.Infrastructure.ResolvingDependencies;
using CsvMapper.UI.Annotations;
using CsvMapper.UI.Utilities;

namespace CsvMapper.UI
{
    class MainViewModel : INotifyPropertyChanged
    {

        private string _inputFileLocation;
        private string _resultsViewContent;
        private string _webPageLocation;

        public MainViewModel()
        {
            _inputFileLocation = "<Click here to locate file>";
            _resultsViewContent = "Waiting to Process..";
            _webPageLocation = "http://www.gpss.org.uk/PatientChase/Banner";
        }


        public string FindFileLocation
        {
            get { return _inputFileLocation; }
            set
            {
                if (_inputFileLocation != value)
                {
                    _inputFileLocation = value;
                    OnPropertyChanged("FindFileLocation");
                }
            }
        }

        public string ResultsViewContent
        {
            get { return _resultsViewContent; }
            set
            {
                if (_resultsViewContent != value)
                {
                    _resultsViewContent = value;
                    OnPropertyChanged("ResultsViewContent");
                }
            }
        }

        public string WebPageLocation
        {
            get { return _webPageLocation; }
            set
            {
                if (_webPageLocation != value)
                {
                    _webPageLocation = value;
                    OnPropertyChanged("WebPageLocation");
                }
            }
        }

        public void FindFile()
        {
            FindFileLocation = new FindFileDialogue("csv").LocationOfFile;
            if (FindFileLocation == null) FindFileLocation = "<Click here to locate file>";
        }

        public void DoExport()
        {
            try
            {
                ResultsViewContent += "\nCreating Export...";
                //Create ETL Layer
                string xmlFile =
                    Path.Combine(new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.FullName,
                        string.Format("{0}.xml", Path.GetFileNameWithoutExtension(_inputFileLocation)));
                var service = new RepositoryData();
                var theContainer = new ModelDataContainer();

                //Extract
                theContainer = service.DataRepository.Create(this._inputFileLocation, new RepositoryMaps(xmlFile).Maps);
                
                //Transform
                theContainer = service.DataRepository.Transform(new RepositoryMaps(xmlFile).Maps, theContainer);
                
                //Load
                service.DataRepository.Save(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    string.Format("{0}.Output.csv", Path.GetFileNameWithoutExtension(_inputFileLocation))), 
                    theContainer);

                ResultsViewContent += "\nOutput exported to the Desktop...";
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.LogMessage(ex);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
