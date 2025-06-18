
function cardClicked(card) {
    const selectedCardId = card.id; 
    const selectedTier = card.getAttribute("data-tier");

    // Set the hidden input field to the selected tier
    document.getElementById("subTier").value = selectedTier;

    console.log(subTier)
    console.log(selectedTier)
     
    // Remove the "selected" class from all cards
    var allCards = document.querySelectorAll('.card');
    //Add selected to the card selected
    allCards.forEach(function (card) {
        card.classList.remove('selected');
    });
    card.classList.add("selected");

    //Just hides the product cards
    var cards = document.querySelectorAll('.cards');
    //features are the 3 boxes below logo top of page
    var features = document.querySelectorAll('.main-features');

    var choose = document.querySelector('.choose-plan');

    choose.classList.add('hidden');
    cards.forEach(function (card) {
        card.classList.add('hidden');
    });
    features.forEach(function (feature) {
        feature.classList.add('hidden');
    });

    // Collect the data elements you need:
    const discount = card.querySelector('.strikedTxt').textContent.trim();
    const planType = card.querySelector('h1').textContent.trim();
    const price = card.querySelector('.cents').textContent.trim();
    const img = card.querySelector('img');
    const imgURL = img ? img.src : '';

    //Store slected card data in obj
    const cardData = {
        discount: discount,
        planType: planType,
        price: price,
        imgURL: imgURL
    };
    subTier.nodeValue = planType;
    console.log("Card Data:", cardData);

    //Show step 2
    document.querySelector('.step-2-container').style.display = 'flex';
    const secondCard = document.querySelector('.second-card');
    secondCard.querySelector('.discount').innerHTML = cardData.discount;
    secondCard.querySelector('.second-card-plan-type').innerHTML = cardData.planType;
    secondCard.querySelector('.offer-num').innerHTML = cardData.price;
    const secondCardImg = secondCard.querySelector('.second-card-image'); // Assuming you have an <img> tag with this class
    if (secondCardImg) {
        secondCardImg.src = cardData.imgURL;
    }
}
 
           


const backButton = document.getElementById('back-button');
backButton.addEventListener('click', function () {
    // Hide the page-2 card
    document.querySelector('.step-2-container').style.display = "none";

    // Show the subscriptions (replace '.subscriptions' with the correct class)
    document.querySelector('.cards').classList.remove('hidden');
    document.querySelector('.choose-plan').classList.remove('hidden');

    var features = document.querySelectorAll('.main-features');
    features.forEach(function (feature) {
        feature.classList.remove('hidden');
    });
});


