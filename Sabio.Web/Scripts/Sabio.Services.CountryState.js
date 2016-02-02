if (!sabio.services.CountryState) {

	sabio.services.CountryState = {};
}

sabio.services.CountryState.getCountries = function (onSuccess, onError) {

	var url = "/api/Addresses/Countries/";
	var settings = {
		cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
	};

	$.ajax(url, settings);

}

sabio.services.CountryState.getStateProvince = function (id, onSuccess, onError) {

	var url = "/api/Addresses/States/" + id;
	var settings = {
	cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    , dataType: "json"
    , success: onSuccess
    , error: onError
    , type: "GET"
	};

	$.ajax(url, settings);
}