using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using ReserGO.DTO;
using ReserGO.DTO.ListAvailability;

namespace ReserGO.Service.ComponentService
{
    public class DaySliderService : ComponentBase
    {
        protected IJSRuntime? JSRuntime { get; private set; }
        [Parameter]
        public EventCallback<(List<DTOTimeSlot> slots, bool next)> Callback { get; set; }
        [Parameter]
        public EventCallback Reset { get; set; }
        [Parameter]
        public List<DTOTimeSlot> SelectedTimeSlot { get; set; }
        [Parameter]
        public bool IsSmallView { get; set; }


        //Solo se usato dal componente DaySliderSpecificDay
        public DTOAvailabilityAdv NewAvailabilityAdv { get; set; }
        private DateTime? _newDate { get; set; }
        public DateTime? newDate { get => _newDate; set
            {
                _newDate = value;
                UpdateTimeSlot();
            }
        }
        private DTOUnavailableTimeDateSlot? _timeSlot { get; set; }
        public DTOUnavailableTimeDateSlot? timeSlot
        {
            get
            {
                if (_timeSlot == null)
                    _timeSlot = new() { SpecificDate = DateTime.Now, TimeSlots = new() };

                return _timeSlot;
            }
            set
            {
                _timeSlot = value;
            }
        }
        //



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitializeBar();
            }
        }

        public async Task InitializeBar()
        {
            if (SelectedTimeSlot != null && SelectedTimeSlot.Count() > 0)
            {
                HandleValues = ConvertTimeSlotsToFloats(SelectedTimeSlot);
            }
            await JSRuntime.InvokeVoidAsync("initializeSlider", DotNetObjectReference.Create(this), HandleValues);
        }

        public void SetJsRuntime(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }

        public List<float> HandleValues = new List<float> { 540, 1080 }; // Intervallo dalle 9:00 alle 18:00

        [JSInvokable]
        public async Task UpdateHandleValues(List<string> values)
        {
            HandleValues = values.Select(v => ConvertTimeStringToFloat(v)).ToList();
            if(NewAvailabilityAdv!=null)
            {
                var ts =NewAvailabilityAdv.UnavailableTimeDatesSlot.SingleOrDefault(x => x.SpecificDate == newDate.Value);
                int idx = NewAvailabilityAdv.UnavailableTimeDatesSlot.IndexOf(ts);
                if (idx != -1)
                    NewAvailabilityAdv.UnavailableTimeDatesSlot[idx].TimeSlots = ConvertDoublesToTimeSlot();
            }
            StateHasChanged();
        }

        public List<DTOTimeSlot> ConvertDoublesToTimeSlot()
        {
            var timeSlots = new List<DTOTimeSlot>();

            for (int i = 0; i < HandleValues.Count; i += 2)
            {
                if (i + 1 < HandleValues.Count)
                {
                    // Converti i double in minuti a TimeSpan
                    var startTime = TimeSpan.FromHours(HandleValues[i]);
                    var endTime = TimeSpan.FromHours(HandleValues[i + 1]);

                    // Crea un nuovo DTOTimeSlot con i valori di inizio e fine
                    var timeSlot = new DTOTimeSlot
                    {
                        StartTime = startTime,
                        EndTime = endTime
                    };

                    timeSlots.Add(timeSlot);
                }
            }

            return timeSlots;
        }

        public float ConvertTimeStringToFloat(string timeString)
        {
            var timeSpan = TimeSpan.Parse(timeString);
            return (float)(timeSpan.Hours + timeSpan.Minutes / 60.0);
        }

        public List<float> ConvertTimeSlotsToFloats(List<DTOTimeSlot> timeSlots)
        {
            var floatList = new List<float>();

            foreach (var slot in timeSlots)
            {
                // Calcola le ore in formato float (ore + minuti / 60)
                float startHour = (float)slot.StartTime.TotalHours * 60; // Converte StartTime in ore
                float endHour = (float)slot.EndTime.TotalHours * 60;     // Converte EndTime in ore

                floatList.Add(startHour);
                floatList.Add(endHour);
            }

            return floatList;
        }


        public async Task RemoveStep()
        {

            if (HandleValues.Count > 2) // Controlla che ci siano almeno due elementi da rimuovere
            {
                HandleValues = HandleValues.Select(x => x * 60).ToList();

                HandleValues.RemoveRange(HandleValues.Count - 2, 2);

                await JSRuntime.InvokeVoidAsync("reinitializeSlider", DotNetObjectReference.Create(this), HandleValues);
            }
        }


        public bool DisableAdd()
        {
            var lastValue = HandleValues.OrderByDescending(x => x).FirstOrDefault();
            lastValue = lastValue < 60 ? lastValue * 60 : lastValue;
            if ((lastValue + 120) >= 1440)
                return true;
            return false;
        }


        public async Task AddStep()
        {
            HandleValues = HandleValues.Select(x => x * 60).ToList();
            // Aggiunge un intervallo extra
            if (HandleValues.Count % 2 == 0) // Aggiunge solo in coppia (inizio e fine)
            {
                var lastValue = HandleValues.OrderByDescending(x => x).FirstOrDefault();
                if ((lastValue + 120) < 1440)
                {
                    HandleValues.Add(lastValue + 60);
                    HandleValues.Add(lastValue + 120);
                    await JSRuntime.InvokeVoidAsync("reinitializeSlider", DotNetObjectReference.Create(this), HandleValues);
                }
            }
        }

        public async Task UpdateSlider()
        {
            HandleValues = ConvertTimeSlotsToFloats(SelectedTimeSlot);
            await JSRuntime.InvokeVoidAsync("reinitializeSlider", DotNetObjectReference.Create(this), HandleValues);
        }

        private void UpdateTimeSlot()
        {
            var ts = NewAvailabilityAdv.UnavailableTimeDatesSlot.SingleOrDefault(x => x.SpecificDate == newDate);
            int idx = NewAvailabilityAdv.UnavailableTimeDatesSlot.IndexOf(ts);
            if (idx != -1)
            {
                try
                {
                    timeSlot = NewAvailabilityAdv.UnavailableTimeDatesSlot.ToList()[idx];
                    SelectedTimeSlot = timeSlot.TimeSlots;
                    UpdateSlider();
                }
                catch (Exception)
                {

                }
            }
        }

    }
}
