
let products = [];
let image = "";

setProductToHTML();
setShoppingCardCountUsingLocalStorage();

async function setProductToHTML(){
  const result = await axios.get("http://localhost:5001/products");

  products = result.data;

  const productsRowElement = $("#productsRow");
    productsRowElement.html("");
  let html = "";
    for(const index in products){
        const product = products[index];

        let buttonText = `
          <button class="btn btn-danger w-100" disabled>
            <i class="bi bi-exclamation-diamond-fill"></i>
            No Product in Stock            
          </button>
          `;

        if(product.stock > 0){
          buttonText = `
          <button onclick="addShoppingCard(${index})" class="btn btn-outline-dark w-100">
            <i class="bi bi-cart-plus"></i>
            Add Shopping Cart
          </button>`
        }

        const text = `
        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 col-12 mt-1">
        <div class="card">
          <div class="card-body product-image-div">
            <img src="${product.image}" alt="" style="width: 100%; max-height:100%">
          </div>
          <div class="card-header product-name-div" style="flex-direction: column">
            <h6>${product.name.substring(0,84)}</h6>
            <span>Stock: ${product.stock}</span>
          </div>
          <div class="card-body text-center">
            <h4 class="alert alert-danger">
              ${formatCurrency(product.price)}
            </h4>
              ${buttonText}
          </div>
        </div>
        </div>`

        html += text;       
    };  

    productsRowElement.html(html);
}

function getImage(e){
  const file = e.target.files[0];

  const reader = new FileReader();  

  reader.onload = function(event){
    image = event.target.result;
  }

  reader.readAsDataURL(file);
}

async function save(e){
    e.preventDefault();
    const nameElement = document.getElementById("name");
    const priceElement = document.getElementById("price");    
    const stockElement = document.getElementById("stock");
    //const id = products.length + 1;

    const product = {
        //id: id,
        name: nameElement.value,
        price: priceElement.value,
        image: image,
        stock: stockElement.value
    };

    await axios.post("http://localhost:5001/products", product);
}

async function addShoppingCard(index){
  const product = products[index];
  await axios.post("http://localhost:5001/shoppingcards", product);

  product.stock--;
  await axios.put("http://localhost:5001/products/" + product.id,product)
}

async function setShoppingCardCountUsingLocalStorage(){
    const result = await axios.get("http://localhost:5001/shoppingcards");

    const val = result.data;

    $("#shopping-card-count").html(val.length);
}