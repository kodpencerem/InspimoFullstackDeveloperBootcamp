console.log("Hello, world!");


let name2:string = "Taner Saydam";
name2 = "asdasd";


let firstName : any;

firstName = "Taner";
firstName = 10;

let firstName2;

if(typeof(firstName) === 'string'){
    firstName2 = firstName;
}



const lastName: string | undefined | null| number | boolean | object | Date = undefined;

//string | number | boolean | Date => pirimite type => ilkel tip
//object
//any | unknown

class personel{
    name:string = "";
    lastName: string = "";

    method(){
        this.lastName;
        const age = 35;
    }
}

const per = new personel();
per.lastName;

enum gender{
    male = 1,
    female,
    notMention
}

gender.female;

type Icon = "Değer 1" | "Değer 2" | "Değer 3";

const icon: Icon = "Değer 2";

