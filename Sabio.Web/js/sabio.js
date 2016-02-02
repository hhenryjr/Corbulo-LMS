var sabio = {
    layout: {}
    , page: {
        handlers: {}
        , startUp: null
    }
    , services: { Events: {} }
};

sabio.layout.startUp = function () {


    if (sabio.page.startUp) {
        console.debug("sabio.layout.startUp fired and found sabio.page.startUp");
        sabio.page.startUp();
    }
};

$(document).ready(sabio.layout.startUp);