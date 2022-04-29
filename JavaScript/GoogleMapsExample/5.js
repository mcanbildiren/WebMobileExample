//GeoLocation
let place = null;
let places = [];
let myPosition = null;
let destinationPosition = null;
let directionsService = null;
let directionsRenderer = null;
let wayPoints = [];

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

var initMap = () => {
    if (navigator.geolocation) {
        directionsService = new google.maps.DirectionsService();
        directionsRenderer = new google.maps.DirectionsRenderer();
        navigator.geolocation.getCurrentPosition(showPosition);

    } else {
        console.log("Geolocation is not supported by this browser.");
    }
}

const showPosition = (position) => {
    //console.log(position);
    myPosition = {
        lat: position.coords.latitude,
        lng: position.coords.longitude
    };

    const map = new google.maps.Map(document.getElementById("map"), {
        center: myPosition,
        zoom: 18,
        mapTypeControl: false,
        styles: mapStyleArr // map theme
    });

    const trafficLayer = new google.maps.TrafficLayer();
    trafficLayer.setMap(map);

    directionsRenderer.setMap(map); //çiziciyi haritaya bağladık
    directionsRenderer.setPanel(document.getElementById("mapPanel"));// sonuçlar burada gösterilecek;

    const card = document.getElementById("pac-card");
    const input = document.getElementById("pac-input");
    const options = {
        fields: ["formatted_address", "geometry", "name", "place_id"],
        strictBounds: false,
        types: ["establishment"],
    };

    map.controls[google.maps.ControlPosition.TOP_CENTER].push(card);

    const autocomplete = new google.maps.places.Autocomplete(input, options);
    autocomplete.bindTo("bounds", map);

    const infowindow = new google.maps.InfoWindow();
    const infowindowContent = document.getElementById("infowindow-content");

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
    const placeList = document.getElementById("place-list");
    const placeItem = document.createElement("li");
    const placeButton = document.createElement("ul");
    placeButton.createElement(<button aria-label='delete item' type='button'>X</button>);
    placeList.classList.add("list-group");
    placeList.classList.add("list-group-flush");
    placeItem.classList.add("list-group-item");
    placeItem.classList.add("item-silinecek");
    placeItem.innerHTML = `${place.name}`;
    placeList.appendChild(placeItem);

    placeItem.addEventListener("click", () => {
        placeList.removeChild(placeItem);    
        // for (let i = 0; i < places.length; i++) {
        //     const item = places[i];
        //     if (item.id == place.id) {
        //         places.splice(i, 1);
        //         break;
        //     }
        // }
        places = places.filter(item => item.id !== place.id);
        localStorage.setItem("places", JSON.stringify(places));
    });
}

const getDirections = () => {
    if (places.length == 0) return;

    wayPoints = [];
    for (let i = 0; i < places.length; i++) {
        wayPoints.push({
            stopover: false,
            location: { placeId: places[i].id }
        });
    }
    //console.log(wayPoints);

    directionsService.route({
        origin: myPosition,
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
        } else {
            window.alert("Directions request failed due to " + status);
        }
    });
};

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
//https://snazzymaps.com/style/287720/modest
const mapStyleArr = [
    {
        "featureType": "all",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "on"
            }
        ]
    },
    {
        "featureType": "all",
        "elementType": "labels",
        "stylers": [
            {
                "visibility": "off"
            },
            {
                "saturation": "-100"
            }
        ]
    },
    {
        "featureType": "all",
        "elementType": "labels.text.fill",
        "stylers": [
            {
                "saturation": 36
            },
            {
                "color": "#000000"
            },
            {
                "lightness": 40
            },
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "all",
        "elementType": "labels.text.stroke",
        "stylers": [
            {
                "visibility": "off"
            },
            {
                "color": "#000000"
            },
            {
                "lightness": 16
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
        "featureType": "administrative",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#000000"
            },
            {
                "lightness": 20
            }
        ]
    },
    {
        "featureType": "administrative",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#000000"
            },
            {
                "lightness": 17
            },
            {
                "weight": 1.2
            }
        ]
    },
    {
        "featureType": "landscape",
        "elementType": "geometry",
        "stylers": [
            {
                "color": "#000000"
            },
            {
                "lightness": 20
            }
        ]
    },
    {
        "featureType": "landscape",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#4d6059"
            }
        ]
    },
    {
        "featureType": "landscape",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#4d6059"
            }
        ]
    },
    {
        "featureType": "landscape.natural",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#4d6059"
            }
        ]
    },
    {
        "featureType": "poi",
        "elementType": "geometry",
        "stylers": [
            {
                "lightness": 21
            }
        ]
    },
    {
        "featureType": "poi",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#4d6059"
            }
        ]
    },
    {
        "featureType": "poi",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#4d6059"
            }
        ]
    },
    {
        "featureType": "road",
        "elementType": "geometry",
        "stylers": [
            {
                "visibility": "on"
            },
            {
                "color": "#7f8d89"
            }
        ]
    },
    {
        "featureType": "road",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#7f8d89"
            }
        ]
    },
    {
        "featureType": "road.highway",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#7f8d89"
            },
            {
                "lightness": 17
            }
        ]
    },
    {
        "featureType": "road.highway",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#7f8d89"
            },
            {
                "lightness": 29
            },
            {
                "weight": 0.2
            }
        ]
    },
    {
        "featureType": "road.arterial",
        "elementType": "geometry",
        "stylers": [
            {
                "color": "#000000"
            },
            {
                "lightness": 18
            }
        ]
    },
    {
        "featureType": "road.arterial",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#7f8d89"
            }
        ]
    },
    {
        "featureType": "road.arterial",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#7f8d89"
            }
        ]
    },
    {
        "featureType": "road.local",
        "elementType": "geometry",
        "stylers": [
            {
                "color": "#000000"
            },
            {
                "lightness": 16
            }
        ]
    },
    {
        "featureType": "road.local",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#7f8d89"
            }
        ]
    },
    {
        "featureType": "road.local",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#7f8d89"
            }
        ]
    },
    {
        "featureType": "transit",
        "elementType": "geometry",
        "stylers": [
            {
                "color": "#000000"
            },
            {
                "lightness": 19
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "all",
        "stylers": [
            {
                "color": "#2b3638"
            },
            {
                "visibility": "on"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "geometry",
        "stylers": [
            {
                "color": "#2b3638"
            },
            {
                "lightness": 17
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#24282b"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#24282b"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "labels",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "labels.text",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "labels.text.fill",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "labels.text.stroke",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "labels.icon",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    }
]