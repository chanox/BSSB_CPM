namespace CPM_Project.Models
{
	public class GlobalVariable
	{
		static string? _ConnectionDB;
		static string? _WebRoot;
        static string? _ContentRoot;
        static string? _ServiceAddress;

        public string? ConnectionDB {
			get
			{
				return _ConnectionDB;
			}
			set {
				_ConnectionDB = value;
			}
		}

        public string? WebRoot
        {
            get
            {
                return _WebRoot;
            }
            set
            {
                _WebRoot = value;
            }
        }

        public string? ContentRoot
        {
            get
            {
                return _ContentRoot;
            }
            set
            {
                _ContentRoot = value;
            }
        }

        public string? ServiceAddress
        {
            get
            {
                return _ServiceAddress;
            }
            set
            {
                _ServiceAddress = value;
            }
        }
    }
}

