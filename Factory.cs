using System;
namespace BenLearns
{
    public class Factory
    {
        public Factory()
        {
        }

        private BenLearns.Managers.Algorithms _Algorithms { get; set; }

        public BenLearns.Managers.Algorithms Algorithms 
        {
            get 
            {
                if(_Algorithms == null)
                {
                    _Algorithms = new BenLearns.Managers.Algorithms();
                }
                return _Algorithms;

            }
            set
            {
                _Algorithms = value;
            }

        }


        private BenLearns.Managers.HomeManager _HomeManager { get; set; }

        public BenLearns.Managers.HomeManager HomeManager
        {
            get
            {
                if (_HomeManager == null)
                {
                    _HomeManager = new BenLearns.Managers.HomeManager();
                }
                return _HomeManager;

            }
            set
            {
                _HomeManager = value;
            }

        }



        private BenLearns.Managers.VolunteerManager _VolunteerManager { get; set; }

        internal BenLearns.Managers.VolunteerManager VolunteerManager
        {
            get
            {
                if (_VolunteerManager == null)
                {
                    _VolunteerManager = new BenLearns.Managers.VolunteerManager();
                }
                return _VolunteerManager;

            }
            set
            {
                _VolunteerManager = value;
            }

        }


        private BenLearns.Data.Volunteers _VolunteerData { get; set; }

        internal BenLearns.Data.Volunteers VolunteerData
        {
            get
            {
                if (_VolunteerData == null)
                {
                    _VolunteerData = new BenLearns.Data.Volunteers();
                }
                return _VolunteerData;

            }
            set
            {
                _VolunteerData = value;
            }

        }

        private Helpers.VolunteerDataHelper _VolunteerDataHelper { get; set; }

        internal Helpers.VolunteerDataHelper VolunteerDataHelper
        {
            get
            {
                if (_VolunteerDataHelper == null)
                {
                    _VolunteerDataHelper = new Helpers.VolunteerDataHelper();
                }
                return _VolunteerDataHelper;

            }
            set
            {
                _VolunteerDataHelper = value;
            }

        }
    }
}
