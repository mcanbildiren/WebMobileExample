console.log("2 ok");

// Operatörler

// Matematiksel Operatörler
// + - / * %
// ++ --
// += -= *= /= %=

// Mantıksal Operatörler
// < > <= >= == != === !== & | && ||

// a = a+""; stringe dönüştürme

// NaN : Not a Number : Alınabilecek Hata
// Infinity : Sonuç
function kontrol() {
  var a = 10;
  b = "10";
  console.log("a = " + typeof a);
  console.log("b = " + typeof b);
  if (a == b && typeof a == typeof b) {
    console.log("a=b");
  } else {
    console.log("a!=b");
  }
  console.log(a + b);
  console.log(a * b);
}

function faktoriyel(n) {
  var sonuc = 1;
  for (var i = 1; i <= n; i++) {
    sonuc *= i;
  }
  return sonuc;
}
