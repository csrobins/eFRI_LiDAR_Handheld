
dim strvar   

exportVM "TEST"
debug.print strvar



function ExportVM(var) as string
strvar = strvar & "        public string " & var
strvar = strvar & "        {"
strvar = strvar & "            get => _plot." & var & ";"
strvar = strvar & "            set"
strvar = strvar & "            {"
strvar = strvar & "                _project." & var & " = value ;"
strvar = strvar & "                NotifyPropertyChanged(" &  Chr(34) & var & Chr(34) & ");"
strvar = strvar & "            }"
strvar = strvar & "        }"

end function