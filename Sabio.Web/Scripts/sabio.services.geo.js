
if (!sabio.services.geo) {
    sabio.services.geo = {};
}


sabio.services.geo.geoCode = function (address, onSuccess, onError) {
    var geocoder = new google.maps.Geocoder();

    geocoder.geocode({ 'address': address }, function (results, status) {

        if (status === google.maps.GeocoderStatus.OK) {
            var position = ({
                lat: results[0].geometry.location.lat(),
                lng: results[0].geometry.location.lng()
            });
            onSuccess(position);
            // to access lat and long use position.lat & position.lng;
        }
        else {
            var error = ('Geocode was not successful for the following reason: ' + status);
            onError(error);
        }
       
    })
}

//use this sample to execute
//sabio.services.geo.geoCode(address, sabio.page.onGeoCodeSuccess, sabio.page.onGeoCodeError);

// include ~/Scripts/sabio.services.geo.js
// include https://maps.googleapis.com/maps/api/js?key=AIzaSyAKTReNrWACHZ8T7rughmp_4sgYl0AGn1w&signed_in=true&callbackk=&sensor=false

//sabio.page.startUp = function () {
//    var address = "5101 San Antonio St. Makati City Philippines";
//    sabio.services.geo.geoCode(address, sabio.page.onGeoCodeSuccess, sabio.page.onGeoCodeError);
//}


//sabio.page.onGeoCodeSuccess = function (data) {
//    console.log(data.lat);
//    console.log(data.lng);
//}

//var onError = function (error) {
//    console.log(error);
//}


