(function () {
    if (!window.onScreenResize) {
        window.onScreenResize = {
            listeners: [],
            lastWidth: window.innerWidth,
            firstLoad: true,
            addResizeListener: function (dotnetHelper, breakpoint) {
                this.listeners.push({ dotnetHelper, breakpoint });

                // Inizializza al primo caricamento
                if (this.firstLoad) {
                    this.listeners.forEach(listener => {
                        listener.dotnetHelper.invokeMethodAsync('OnScreenResize', window.innerWidth < listener.breakpoint);
                    });
                    if (this.listener != undefined)
                        this.firstLoad = false;
                }

                // Aggiungi il listener dell'evento resize solo una volta
                if (!this.eventAttached) {
                    window.addEventListener('resize', () => {
                        let currentWidth = window.innerWidth;
                        this.listeners.forEach(listener => {
                            let breakpoint = listener.breakpoint;
                            if ((this.lastWidth >= breakpoint && currentWidth < breakpoint) ||
                                (this.lastWidth < breakpoint && currentWidth >= breakpoint)) {
                                listener.dotnetHelper.invokeMethodAsync('OnScreenResize', currentWidth < breakpoint);
                            }
                        });
                        this.lastWidth = currentWidth;
                    });
                    this.eventAttached = true;
                }
            }
        };
    }
})();


window.initializeSlider = (dotNetHelper, handleValues) => {
    const slider = document.getElementById('slider');

    // Crea un array connect che ha un elemento in più rispetto alle maniglie
    const connect = [];
    for (let i = 0; i < handleValues.length + 1; i++) {
        connect.push(i % 2 === 1); // Colora solo gli spazi tra le maniglie
    }
    var aproximateHour = function (mins) {
        var minutes = Math.round(mins % 60);
        if (minutes == 60 || minutes == 0) {
            return mins / 60;
        }
        return Math.trunc(mins / 60) + minutes / 100;
    }


    noUiSlider.create(slider, {
        start: handleValues,
        connect: connect, // Usa l'array connect generato
        range: { 'min': 0, 'max': 1440 },
        format: wNumb({
            decimals: 2,
            mark: ":",
            encoder: function (a) {
                return aproximateHour(a);
            }
        }),
        step: 30,
        tooltips: true,
    });

    slider.noUiSlider.on('update', (values) => {
        dotNetHelper.invokeMethodAsync('UpdateHandleValues', values);
    });
};

window.reinitializeSlider = (dotNetHelper, handleValues) => {
    const slider = document.getElementById('slider');
    if (slider.noUiSlider) {
        slider.noUiSlider.destroy(); // Distrugge lo slider esistente
    }

    // Crea di nuovo l'array connect
    const connect = [];
    for (let i = 0; i < handleValues.length + 1; i++) {
        connect.push(i % 2 === 1); // Colora solo gli spazi tra le maniglie
    }

    var aproximateHour = function (mins) {
        var minutes = Math.round(mins % 60);
        if (minutes == 60 || minutes == 0) {
            return mins / 60;
        }
        return Math.trunc(mins / 60) + minutes / 100;
    }


    noUiSlider.create(slider, {
        start: handleValues,
        connect: connect, // Usa l'array connect generato
        range: { 'min': 0, 'max': 1440 },
        format: wNumb({
            decimals: 2,
            mark: ":",
            encoder: function (a) {
                return aproximateHour(a);
            }
        }),
        step: 30,
        tooltips: true,
    });
    slider.noUiSlider.on('update', (values) => {
        dotNetHelper.invokeMethodAsync('UpdateHandleValues', values);
    });
};

function filter_hour(value, type) {
    return (value % 60 == 0) ? 1 : 0;
}

function playAlertSound() {
    const audio = new Audio('/sounds/alert.mp3'); // Percorso al file audio
    audio.play();
}