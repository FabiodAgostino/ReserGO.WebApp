using ReserGO.DTO;
using ReserGO.DTO.ListAvailability;

namespace ReserGO.Miscellaneous.Model
{
    public class DTOResourceStepper
    {
        public List<DTODayOfWeekWizard> DaysSelected { get; set; }
        public List<DTOTimeSlot> RecurringRules { get; set; } = new();
        public DTOResource Resource { get; set; }

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
        public ResourceStepperState ActualState { get; set; }
        public Dictionary<int, ResourceStepperState> State { get; set; }

        public enum ResourceStepperState
        {
            DAYS,
            RECURRING_RULES,
            CONFIRM,
            FINISH
        }

        public DTOResourceStepper()
        {
            ActualState = ResourceStepperState.DAYS;
            State = SetState();
        }

        private Dictionary<int, ResourceStepperState> SetState()
        {
            int i = 0;
            var dict = new Dictionary<int, ResourceStepperState>();

            dict.Add(i++, ResourceStepperState.DAYS);
            dict.Add(i++, ResourceStepperState.RECURRING_RULES);
            dict.Add(i++, ResourceStepperState.CONFIRM);
            dict.Add(i, ResourceStepperState.FINISH);


            return dict;
        }
    }
}
