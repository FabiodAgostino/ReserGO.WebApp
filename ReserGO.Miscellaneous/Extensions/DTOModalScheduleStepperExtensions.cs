using ReserGO.DTO;
using ReserGO.DTO.ListAvailability;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.Miscellaneous.Extensions
{
    public static class DTOModalScheduleStepperExtensions
    {
        public static bool Disabled(this DTOModalScheduleStepper stepper, StateOfStepper state)
        {
            int idxStep = stepper.State.SingleOrDefault(x => x.Value == state).Key;
            if (idxStep == 0)
                idxStep = -1;
            return stepper.Index < idxStep;
        }

        public static void UpdateServices(this DTOModalScheduleStepper stepper, List<DTOService> services)
        {
            stepper.Services = services;
            stepper.TotalPrice = services.Sum(s => s.Price.Value);
            if (!stepper.SmallView)
                stepper.NextIndex();
        }
        public static void UpdateDate(this DTOModalScheduleStepper stepper, DateTime date)
        {
            stepper.Date = date;
        }

        public static void UpdateSlot(this DTOModalScheduleStepper stepper, DTOTimeSlot slot)
        {
            stepper.Slot = slot;
            if (!stepper.SmallView)
                stepper.NextIndex();
        }

        public static void UpdateUser(this DTOModalScheduleStepper stepper, DTOUserLight user)
        {
            stepper.User = user;
            stepper.NextIndex();
        }


        public static void NextIndex(this DTOModalScheduleStepper stepper)
        {
            if (stepper.Index < stepper.State.Count())
            {
                stepper.Index++;
            }
        }

        public static void PreviewsIndex(this DTOModalScheduleStepper stepper)
        {
            if (stepper.Index > 0)
            {
                stepper.Index--;
            }
        }

        public static bool DisabledNextButtonSmartphone(this DTOModalScheduleStepper stepper)
        {
            switch (stepper.ActualState)
            {
                case StateOfStepper.SERVICES:
                    return stepper.Services == null || stepper.Services.Count() == 0;
                case StateOfStepper.DATE:
                    return !stepper.Date.HasValue;
                case StateOfStepper.SLOT:
                    return stepper.Slot == null;
                case StateOfStepper.USER:
                    return stepper.User == null;
                case StateOfStepper.CONFIRM:
                    return false;
                default:
                    return false;
            }
        }

        public static void ResetIndex(this DTOModalScheduleStepper stepper, bool smallView=false)
        {

            stepper.SmallView = smallView;
            SetState(stepper);
            while(!stepper.State.Any(x => x.Value == stepper.ActualState))
            {
                stepper.ActualState--;
            }
            stepper.Index = stepper.State.SingleOrDefault(x => x.Value == stepper.ActualState).Key;
        }

        public static bool IsLastButtonSmartphone(this DTOModalScheduleStepper stepper)
            => stepper.ActualState == StateOfStepper.CONFIRM;

        private static void SetState(this DTOModalScheduleStepper stepper)
        {
            int i = 0;
            var dict = new Dictionary<int, StateOfStepper>();
            if (stepper.EnableServices)
            {
                dict.Add(i++, StateOfStepper.SERVICES);
            }

            if (stepper.SmallView)
            {
                dict.Add(i++, StateOfStepper.DATE);
                dict.Add(i++, StateOfStepper.SLOT);
            }
            else
            {
                dict.Add(i++, StateOfStepper.DATESLOT);
            }

            if (!stepper.IsLoggedIn)
            {
                dict.Add(i++, StateOfStepper.USER);
            }

            dict.Add(i, StateOfStepper.CONFIRM);

            stepper.State = dict;
        }
    }
}
