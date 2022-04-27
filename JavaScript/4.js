// geolocation

var getLocation = () => {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(showPosition);
  } else {
    console.log("Geolocation is not supported by this browser.");
  }
};

var showPosition = (position) => {
  console.log(position);
};

//https://developers.google.com/maps/documentation/javascript/cloud-setup