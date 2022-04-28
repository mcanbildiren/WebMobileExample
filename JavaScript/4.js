// geolocation

var initMap = () => {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(showPosition);
  } else {
    console.log("Geolocation is not supported by this browser.");
  }
};

var showPosition = (position) => {
  console.log(position);
  var pos = {
    lat: position.coords.latitude,
    lng: position.coords.longitude,
  };
  var mapDiv = document.getElementById("map");
  var map = new google.maps.Map(mapDiv, {
    center: pos,
    zoom: 18,
    mapTypeId: "terrain",
  });

  var marker = new google.maps.Marker({
    position: pos,
    map: map,
  });

  map.addListener("click", (e) => {
    console.log(e);
    var posClick = {
      lat: e.latLng.lat(),
      lng: e.latLng.lng(),
    };
    //markerları temizle
    marker.setMap(null);
    //tıklanan konuma göre marker ekle
    marker = new google.maps.Marker({
      position: posClick,
      map: map,
      title: 'Clicked Location',
      animation: google.maps.Animation.DROP
    });
  });
};

//https://developers.google.com/maps/documentation/javascript/cloud-setup
