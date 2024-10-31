using ReserGO.DTO.ListAvailability;
using ReserGO.Miscellaneous.Model;
using static ReserGO.Miscellaneous.Model.DTOResourceStepper;

namespace ReserGO.Miscellaneous.Extensions
{
    public static class DTOResourceStepperExtension
    {
        public static bool Disabled(this DTOResourceStepper stepper, ResourceStepperState state)
        {
            int idxStep = stepper.State.SingleOrDefault(x => x.Value == state).Key;
            if (idxStep == 0)
                idxStep = -1;
            return stepper.Index < idxStep;
        }

        public static void UpdateDays(this DTOResourceStepper stepper, List<DTODayOfWeekWizard> days, bool next=false)
        {
            stepper.DaysSelected = days;
            if (next)
                NextIndex(stepper);
        }

        public static void UpdateRecurringRules(this DTOResourceStepper stepper, List<DTOTimeSlot> slots, bool next = false)
        {
            stepper.RecurringRules = slots;
            if (next)
                NextIndex(stepper);
        }

        public static void Confirm(this DTOResourceStepper stepper)
        {
            stepper.ActualState = ResourceStepperState.FINISH;
        }


        public static void NextIndex(this DTOResourceStepper stepper)
        {
            if (stepper.Index < stepper.State.Count())
            {
                stepper.Index++;
            }
        }

        public static void PreviewsIndex(this DTOResourceStepper stepper)
        {
            if (stepper.Index > 0)
            {
                stepper.Index--;
            }
        }
    }
}
