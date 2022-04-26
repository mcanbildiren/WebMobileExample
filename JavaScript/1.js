function mesaj() {
  alert("Merhaba JavaScript");
}

degisken1 = {
  ad: "kamil",
  soyad: "fıdıl",
  yas: 23,
  meslek: "çalışan",
  bilgileriGoster: function () {
    return this.ad + " " + this.soyad + " " + this.yas + " " + this.meslek;
  },
};
