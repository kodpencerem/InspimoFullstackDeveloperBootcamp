let shoppingCards = [];
let totalAmount = 0;

getAll();

async function getAll(){
  var result = await axios.get("http://localhost:5001/shoppingcards");

  shoppingCards = result.data;

  for(let card of shoppingCards){
    totalAmount += +card.price;
  }

  setShoppingCardToHTML();
  setShoppingCardCount();
}

function setShoppingCardToHTML(){
    const shoppingCardsRowElement = document.getElementById("shoppingCardsRow");
    shoppingCardsRowElement.innerHTML = "";

    for(const index in shoppingCards){
        const shoppingCard = shoppingCards[index];
        const text = `
        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-8 col-12 mt-1">
        <div class="card">
          <div class="card-body product-image-div">
            <img src="${shoppingCard.image}" alt="" style="width: 100%; max-height:100%">
          </div>
          <div class="card-header product-name-div">
            <h6>${shoppingCard.name.substring(0,84)}</h6>
          </div>
          <div class="card-body text-center">
            <h4 class="alert alert-success">
              ${formatCurrency(shoppingCard.price)}
            </h4>
            <button onclick="deleteByIndex('${shoppingCard.id}')" class="btn btn-outline-danger w-100">
              <i class="bi bi-trash"></i>
              Delete
            </button>
          </div>
        </div>
        </div>`

        if(shoppingCardsRowElement !== null){
            shoppingCardsRowElement.innerHTML += text;
        }
    }

    const totalAmountEl = document.getElementById("totalAmount");
    totalAmountEl.innerHTML = formatCurrency(totalAmount);
}

function setShoppingCardCount(){
    const shoppingCardCountElement = document.getElementById("shopping-card-count");

    shoppingCardCountElement.innerHTML =shoppingCards.length;
}

async function deleteByIndex(id){
  const res = await Swal.fire({
    title: 'Delete!',
    text: 'Do you want to delete',
    icon: 'question',
    confirmButtonText: 'Delete',
    showConfirmButton: true,
    showCancelButton: true,
    cancelButtonText: "Cancel"
  });

  if(res.isConfirmed){
    await fetch("http://localhost:5001/shoppingcards/" + id, {
      method: "DELETE"
    });
  }  
}

function payAndCreateOrder(e){
  e.preventDefault();

  Swal.fire({
    title: 'Pay?',
    text: 'Do you want to but this products',
    icon: 'question',
    confirmButtonText: 'Buy',
    showConfirmButton: true,
    showCancelButton: true,
    cancelButtonText: "Cancel"
  }).then(async res=> {
    if(res.isConfirmed){  
      for(let card of shoppingCards){
        await fetch("http://localhost:5001/shoppingcards/" + card.id, {
          method: "DELETE"
        });
      }
    }
  }) 
}