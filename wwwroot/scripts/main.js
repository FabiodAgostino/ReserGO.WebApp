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
                    if(this.listener!=undefined)
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
