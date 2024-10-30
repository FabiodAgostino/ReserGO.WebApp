using ReserGO.DTO;
using ReserGO.DTO.ListAvailability;

namespace ReserGO.Miscellaneous.Model
{
    public class DTOModalScheduleStepper
    {
        public List<DTOService> Services { get; set; } = new();
        public bool EnableServices { get; set; } = true;
        public DateTime? Date { get; set; }
        public DTOTimeSlot Slot { get; set; } = new();
        public DTOUserLight User { get; set; } = new();
        public bool IsLoggedIn { get; set; } = true;

        private int _index { get; set; }
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                ActualState = State.SingleOrDefault(x => x.Key == value).Value;
            }
        }
        public bool SmallView { get; set; }
        public Dictionary<int, StateOfStepper> State { get; set; }
        public StateOfStepper ActualState { get; set; }
        public string ResourceName { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool RequestToConfirm { get; set; }


        public DTOModalScheduleStepper()
        {
            
        }

        public DTOModalScheduleStepper(DTOResource resource, bool isLoggedIn, bool smallView)
        {
            ResourceName = resource.ResourceName;
            RequestToConfirm = resource.RequestToConfirm;
            bool enableServices = resource.Services is object && resource.Services.Count() > 0;
            if (enableServices)
                Services = new();
            EnableServices = enableServices;
            IsLoggedIn = isLoggedIn;
            SmallView = smallView;
            State = SetState();
            ActualState = State.FirstOrDefault().Value;

        }

        private Dictionary<int, StateOfStepper> SetState()
        {
            int i = 0;
            var dict = new Dictionary<int, StateOfStepper>();
            if (EnableServices)
            {
                dict.Add(i++, StateOfStepper.SERVICES);
            }

            if (SmallView)
            {
                dict.Add(i++, StateOfStepper.DATE);
                dict.Add(i++, StateOfStepper.SLOT);
            }
            else
            {
                dict.Add(i++, StateOfStepper.DATESLOT);
            }

            if (!IsLoggedIn)
            {
                dict.Add(i++, StateOfStepper.USER);
            }

            dict.Add(i, StateOfStepper.CONFIRM);

            return dict;
        }
    }



    public enum StateOfStepper
    {
        SERVICES,
        DATESLOT,
        DATE,
        SLOT,
        USER,
        CONFIRM
    }
}
