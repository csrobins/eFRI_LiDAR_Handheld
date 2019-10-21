
strvar = {}

function ExportVM(var) as string
        public string PROJECTID
        {
            get => _project.PROJECTID;
            set
            {
                _project.PROJECTID = value ;
                NotifyPropertyChanged("PROJECTID");
            }
        }