var sabio = {
    layout: {},
    page: {
        handlers: {},
        startUp: null
    },
    services: {},
    ui: {
        notifications: {},
        startUp: null
    }
};


sabio.layout.startUp = function () {

    //this does a null check on sabio.page.startUp
    if (sabio.page.startUp) {
        console.log("sabio.page.startup");
        sabio.page.startUp();
    }
};
$(document).ready(sabio.layout.startUp);