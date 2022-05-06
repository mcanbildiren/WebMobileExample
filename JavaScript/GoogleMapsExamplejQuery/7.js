let place = null;
let places = [];
let myPosition = null;
let destinationPosition = null;
let directionsService = null;
let directionsRenderer = null;
let wayPoints = [];

var initMap = () => {
    if (navigator.geolocation) {
        directionsService = new google.maps.DirectionsService();
        directionsRenderer = new google.maps.DirectionsRenderer();
        navigator.geolocation.getCurrentPosition(showPosition);

    } else {
        console.log("Geolocation is not supported by this browser.");
    }
}
const checkLocalStorage = () => {
    if (localStorage.getItem("places") === null) {
        localStorage.setItem("places", JSON.stringify(places));
    } else {
        places = JSON.parse(localStorage.getItem("places"));
        for (let i = 0; i < places.length; i++) {
            let place = places[i];
            addPlaceHtml(place);
        }
    }
};

const showPosition = (position) => {
    $("#place-list-container,#pac-card").slideDown(1000);
    //console.log(position);
    myPosition = {
        lat: position.coords.latitude,
        lng: position.coords.longitude
    };
    const map = new google.maps.Map($("#map")[0], {
        center: myPosition,
        zoom: 18,
        mapTypeControl: false,
        styles: mapStyleArr
    });

    const trafficLayer = new google.maps.TrafficLayer();
    trafficLayer.setMap(map);

    directionsRenderer.setMap(map); //çiziciyi haritaya bağladık
    directionsRenderer.setPanel($("#mapPanel")[0]);// sonuçlar burada gösterilecek;

    const card = $("#pac-card")[0];
    const input = $("#pac-input")[0];
    const options = {
        fields: ["formatted_address", "geometry", "name", "place_id"],
        strictBounds: false,
        types: ["establishment"],
    };

    map.controls[google.maps.ControlPosition.TOP_CENTER].push(card);

    const autocomplete = new google.maps.places.Autocomplete(input, options);
    autocomplete.bindTo("bounds", map);

    const infowindow = new google.maps.InfoWindow();
    const infowindowContent = $("#infowindow-content")[0];

    infowindow.setContent(infowindowContent);

    const marker = new google.maps.Marker({
        map,
        anchorPoint: new google.maps.Point(0, -29),
    });

    autocomplete.addListener("place_changed", () => {
        infowindow.close();
        marker.setVisible(false);

        place = autocomplete.getPlace();
        // console.log(place);
        if (!place.geometry || !place.geometry.location) {
            alert("No details available for input: '" + place.name + "'");
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);
        }

        destinationPosition = {
            lat: place.geometry.location.lat(),
            lng: place.geometry.location.lng(),
        };

        //getDirections(directionsService, directionsRenderer);

        marker.setPosition(place.geometry.location);
        marker.setVisible(true);
        infowindowContent.children["place-name"].textContent = place.name;
        infowindowContent.children["place-address"].textContent =
            place.formatted_address;
        infowindow.open(map, marker);
    });
}
$(() => { // on page load -- document.ready
    $("#btn-ekle").on("click", addPlace);
    $("#btn-rota-olustur").on("click", getDirections);
    checkLocalStorage();
    $("#place-list-container,#pac-card").hide();
});
const getDirections = () => {
    if (places.length == 0) return;

    wayPoints = [];
    for (let i = 0; i < places.length; i++) {
        wayPoints.push({
            stopover: true,
            location: { placeId: places[i].id }
        });
    }
    //console.log(wayPoints);

    directionsService.route({
        origin: myPosition,
        //destination: wayPoints[wayPoints.length - 1].location,
        destination: myPosition,
        travelMode: "DRIVING",
        waypoints: wayPoints,
        optimizeWaypoints: true,
        drivingOptions: {
            departureTime: new Date(Date.now()),
            trafficModel: "bestguess"
        }
    }, (response, status) => {
        if (status === "OK") {
            //console.log(response);
            directionsRenderer.setDirections(response);
            $("#myAudio")[0].play();// rota oluşturuldu. wissen iyi yolculuklar diler
        } else {
            window.alert("Directions request failed due to " + status);
        }
    });
};

const addPlace = () => {
    if (place != null) {
        var venue = {
            name: place.name,
            id: place.place_id,
        };

        if (checkPlaces(venue)) {
            place = null;
            return;
        }

        places.push(venue);
        localStorage.setItem("places", JSON.stringify(places));
        // console.log(places);
        addPlaceHtml(venue);
        place = null;
    }
}

const addPlaceHtml = (place) => {
    const placeList = $("#place-list");
    const placeItem = $("<li>"); //document.createElement("li");
    const deleteBtn = $("<input>"); //document.createElement("input");

    placeList.addClass("list-group").addClass("list-group-flush");
    placeItem.addClass("list-group-item").addClass("item-silinecek").addClass("d-flex").addClass("justify-content-between");

    deleteBtn.addClass("btn").addClass("btn-outline-danger").addClass("btn-sm");
    deleteBtn.attr("type", "button").attr("value", "SİL");

    placeItem.html(`${place.name}`);
    placeItem.append(deleteBtn);
    placeList.append(placeItem);

    deleteBtn.on("click", () => {
        Swal.fire({
            title: 'Emin misiniz?',
            text: `${place.name} silinecek!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet!',
            cancelButtonText: 'Hayır!'
        }).then((result) => {
            if (result.isConfirmed) {
                placeItem.remove(); //place item nesnesini direk domdan kaldırır
                places = places.filter(item => item.id !== place.id);
                localStorage.setItem("places", JSON.stringify(places));
            }
        });
    });
}

const checkPlaces = (venue) => {
    //console.log([places, venue]);
    for (let i = 0; i < places.length; i++) {
        const item = places[i];
        if (item.id === venue.id) {
            return true;
        }
    }
    return false;
};

const mapStyleArr = [
    {
        "featureType": "all",
        "elementType": "geometry",
        "stylers": [
            {
                "color": "#202c3e"
            }
        ]
    },
    {
        "featureType": "all",
        "elementType": "labels.text.fill",
        "stylers": [
            {
                "gamma": 0.01
            },
            {
                "lightness": 20
            },
            {
                "weight": "1.39"
            },
            {
                "color": "#ffffff"
            }
        ]
    },
    {
        "featureType": "all",
        "elementType": "labels.text.stroke",
        "stylers": [
            {
                "weight": "0.96"
            },
            {
                "saturation": "9"
            },
            {
                "visibility": "on"
            },
            {
                "color": "#000000"
            }
        ]
    },
    {
        "featureType": "all",
        "elementType": "labels.icon",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "landscape",
        "elementType": "geometry",
        "stylers": [
            {
                "lightness": 30
            },
            {
                "saturation": "9"
            },
            {
                "color": "#29446b"
            }
        ]
    },
    {
        "featureType": "poi",
        "elementType": "geometry",
        "stylers": [
            {
                "saturation": 20
            }
        ]
    },
    {
        "featureType": "poi.park",
        "elementType": "geometry",
        "stylers": [
            {
                "lightness": 20
            },
            {
                "saturation": -20
            }
        ]
    },
    {
        "featureType": "road",
        "elementType": "geometry",
        "stylers": [
            {
                "lightness": 10
            },
            {
                "saturation": -30
            }
        ]
    },
    {
        "featureType": "road",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#193a55"
            }
        ]
    },
    {
        "featureType": "road",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "saturation": 25
            },
            {
                "lightness": 25
            },
            {
                "weight": "0.01"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "all",
        "stylers": [
            {
                "lightness": -20
            }
        ]
    }
];