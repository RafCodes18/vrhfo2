/*            MOBILE RESPONSIVENESS STUFF
const button = document.getElementById('fixed-button');

function handleResize() {
    if (window.matchMedia('(max-width: 760px)').matches) {
        button.style.display = 'block'; // Show the button
    } else {
        button.style.display = 'none';  // Hide the button
    }
}*/
/*
// Event listeners for both resize and initial display state
window.addEventListener('resize', handleResize);
handleResize();*/

function cardClicked(card) {
    const selectedCardId = card.id; 
    console.log(selectedCardId)

    // Remove the "Continue" button from all cards
    var allContinueButtons = document.querySelectorAll('.button-continue');
    allContinueButtons.forEach(function (button) {
        button.style.display = 'none';
    });

    // Remove the overlay from all cards
    var allOverlays = document.querySelectorAll('.hover-overlay');
    allOverlays.forEach(function (overlay) {
        overlay.style.opacity = '0';
    });



    // Add the "Continue" button to the clicked card
    var continueButton = card.querySelector('.button-continue');
    if (continueButton) {
        continueButton.style.display = 'flex';
    }

    // Keep the overlay visible for the clicked card
    var overlay = card.querySelector('.hover-overlay');
    if (overlay) {
        overlay.style.opacity = '1';
    }

    // Remove the "selected" class from all cards
    var allCards = document.querySelectorAll('.card');
    allCards.forEach(function (card) {
        card.classList.remove('selected');
    });
    card.classList.add("selected");


}


// Runs when  Continue button (ADDED DYNAMICALLY TO BOTOTM OF CARDS) is clicked

    var continueButtons = document.querySelectorAll('.continue-button');
    continueButtons.forEach(function (button) {
        button.addEventListener('click', function () {
                var pages = document.querySelectorAll('.main-product-cards');
                var features = document.querySelectorAll('.main-features');
                pages.forEach(function (page) {
                    page.classList.add('hidden');
                });
                features.forEach(function (feature) {
                    feature.classList.add('hidden');
                });

            console.log(button.id);



            const card = button.closest('.card'); // Find the closest ancestor with the 'card' class

            // Collect the data elements you need:
            const discount = card.querySelector('.discount').textContent.trim();
            const planType = card.querySelector('h2').textContent.trim();
            const price = card.querySelector('.offer-num').textContent.trim();
            const imgContainer = card.querySelector('.img-container');
            const imgElement = imgContainer.querySelector('img');
            const imgURL = imgElement ? imgElement.src : ''; // Get the 'src' if available

            // Example: Store the data in an object
            const cardData = {
                discount: discount,
                planType: planType,
                price: price,
                imgURL: imgURL
            };

            console.log("Card Data:", cardData); 

            document.querySelector('.step-2').style.display = 'block';

            const secondCard = document.querySelector('.second-card');
            secondCard.querySelector('.discount').innerHTML = cardData.discount;
            secondCard.querySelector('.second-card-plan-type').innerHTML = cardData.planType;
            secondCard.querySelector('.offer-num').innerHTML = cardData.price;
            const secondCardImg = secondCard.querySelector('.second-card-image'); // Assuming you have an <img> tag with this class
            if (secondCardImg) {
                secondCardImg.src = cardData.imgURL;
            }
        });
    });
