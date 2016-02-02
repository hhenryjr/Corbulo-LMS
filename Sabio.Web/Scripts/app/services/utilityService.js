sabio.services.utilityFactory = function ($baseService)
{    
    var svc = this;
    svc = $baseService.merge(true, {}, svc, $baseService);

    svc.militaryTimeFromDate = _militaryTimeFromDate;
    svc.dateFromMilitaryTime = _dateFromMilitaryTime;
    svc.serializeDatepicker = _serializeDatepicker;
    svc.createDateAsUTC = _createDateAsUTC;
    svc.convertDateToUTC = _convertDateToUTC;
    svc.convertStringDateToUTC = _convertStringDateToUTC;
    svc.convertToSlug = _convertToSlug;
    //svc.newSlug = final;

    function _convertToSlug(text)
    {
        var lowerCase = text.toLowerCase();
        var firstReplace = lowerCase.replace(/[^\w ]+/g, '');
        var final = firstReplace.replace(/ +/g, '-');
        svc.newSlug = final;
        return svc.newSlug;
    }
    
    function _convertStringDateToUTC(dateString)
    {
        var date = new Date(dateString);

        return _convertDateToUTC(date);
    }

    //  UTC helper functions adapted from this SO: http://stackoverflow.com/a/14006555
    function _createDateAsUTC(date) {
        return new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds()));
    }

    function _convertDateToUTC(date) {
        return new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
    }

    function _serializeDatepicker(date)
    {        
        return date.toLocaleDateString("en-US");
    }
    
    function _militaryTimeFromDate(date) {
        var hrs = date.getHours().toString();

        if (hrs.length == 1)
            hrs = "0" + hrs;

        var mins = date.getMinutes().toString();

        if (mins.length == 1)
            mins = "0" + mins;

        return hrs + mins;
    }

    function _dateFromMilitaryTime(time) {
        time = time.toString();

        if (time.length == 3)
            time = "0" + time;

        var date = new Date();

        var hrs = 9;
        var min = 0;

        if (time) {
            hrs = parseInt(time.substring(0, 2));
            min = parseInt(time.substring(2, 4));
        }

        date.setHours(hrs);
        date.setMinutes(min);

        return date;
    }

    return svc;
}

sabio.ng.addService(sabio.ng.app.module
    , "$utilityService"
    , ["$baseService"]
    , sabio.services.utilityFactory);