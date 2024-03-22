let products = []; //array
const shoppingCards = [];

if(localStorage.getItem("products")){
    products = JSON.parse(localStorage.getItem("products"));
}

setProductToHTML();
setShoppingCardCountUsingLocalStorage();

function setProductToHTML(){
    const productsRowElement = document.getElementById("productsRow");
    productsRowElement.innerHTML = "";

    for(const index in products){
        const product = products[index];
        const text = `
        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 col-12 mt-1">
        <div class="card">
          <div class="card-body product-image-div">
            <img src="${product.image}" alt="" style="width: 100%; max-height:100%">
          </div>
          <div class="card-header product-name-div">
            <h6>${product.name.substring(0,84)}</h6>
          </div>
          <div class="card-body text-center">
            <h4 class="alert alert-danger">
              ${product.price}
            </h4>
            <button onclick="addShoppingCard(${index})" class="btn btn-outline-dark w-100">
              <i class="bi bi-cart-plus"></i>
              Add Shopping Cart
            </button>
          </div>
        </div>
        </div>`

        if(productsRowElement !== null){
            productsRowElement.innerHTML += text;
        }
    }
}

function save(event){
    event.preventDefault();
    const nameElement = document.getElementById("name");
    const priceElement = document.getElementById("price");
    const imageElement = document.getElementById("image");

    const product = {
        name: nameElement.value,
        price: priceElement.value,
        image: imageElement.value
    };

    products.push(product);

    localStorage.setItem("products",JSON.stringify(products));

    nameElement.value = "";
    priceElement.value = "";
    imageElement.value = "";

    const closeBtnElement = document.getElementById("addProductModalCloseBtn");
    closeBtnElement.click();

    setProductToHTML();

    const toastrOptions = {
        closeButton: true,
        progressBar: true,
        positionClass: "toast-bottom-right"
    }
    toastr.options = toastrOptions;
    toastr.success("Product add is successful");
    //warning | info | danger | success
}

function addShoppingCard(index){
    const product = products[index]
    shoppingCards.push(product);

    localStorage.setItem("shoppingCards", JSON.stringify(shoppingCards));

    setShoppingCardCountUsingLocalStorage();
}

function setShoppingCardCountUsingLocalStorage(){
    let cards = [];
    if(localStorage.getItem("shoppingCards")){
        cards = JSON.parse(localStorage.getItem("shoppingCards"));
    }

    const shoppingCardCountElement = document.getElementById("shopping-card-count");

    shoppingCardCountElement.innerHTML =cards.length;
}