function save(event){
    event.preventDefault();
    const firstNameInputElement = document.getElementById("firstName");
    const lastNameInputElement = document.getElementById("lastName");
    const professionInputElement = document.getElementById("profession");
    const startDateInputElement = document.getElementById("startDate");
    const salaryInputElement = document.getElementById("salary");

    const data = {
        firstName: firstNameInputElement.value,
        lastName: lastNameInputElement.value,
        profession: professionInputElement.value,
        startDate: startDateInputElement.value,
        salary: salaryInputElement.value
    }

    console.log(data);
}