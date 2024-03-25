let data = {};

apiRequest();

function apiRequest(){
    fetch("./db.json").then(res=> res.json()).then(response => {
        data = response;
        changeAvatar();
        changeSubTitle();
        changeSkills();
        changeAboutMe();
        changeSocialMedias();
    });
}

function changeAvatar(){
    const el = document.getElementById("avatar");
    el.src = data.avatar;
}

function changeSubTitle(){
    const el = document.getElementById("subtitle");
    el.innerText = data.subtitle;
}

function changeSkills(){
    const el = document.querySelector(".skills");

    let text = "";

    for(let skill of data.skills){
        text += `
        <img src="${skill}" width="150">
        `
    }

    el.innerHTML = text;
}

function changeAboutMe(){
    const el = document.getElementById("aboutMeContent");
    el.innerHTML = data.aboutMe;
}

function changeSocialMedias(){
    const el = document.querySelector(".socialMedias");

    let text = "";
    for(let sc of data.socialMedias){
        text += `
        <a href="${sc.url}" target="_blank" title="${sc.name}">
            <i class="${sc.icon}"></i>
        </a>
        `
    }

    el.innerHTML = text;
}


