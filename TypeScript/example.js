console.log("Hello, world!");
var name2 = "Taner Saydam";
name2 = "asdasd";
var firstName;
firstName = "Taner";
firstName = 10;
var firstName2;
if (typeof (firstName) === 'string') {
    firstName2 = firstName;
}
var lastName = undefined;
//string | number | boolean | Date => pirimite type => ilkel tip
//object
//any | unknown
var personel = /** @class */ (function () {
    function personel() {
        this.name = "";
        this.lastName = "";
    }
    personel.prototype.method = function () {
        this.lastName;
        var age = 35;
    };
    return personel;
}());
var per = new personel();
per.lastName;
var gender;
(function (gender) {
    gender[gender["male"] = 0] = "male";
    gender[gender["female"] = 1] = "female";
    gender[gender["notMention"] = 2] = "notMention";
})(gender || (gender = {}));
gender.female;
var icon = "Değer 2";
