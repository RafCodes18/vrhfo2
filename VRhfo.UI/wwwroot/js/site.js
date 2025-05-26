const searchCue = document.querySelector('.btn-outline-primary');
const searchBar = document.querySelector('.custom-search');
const mobileSearchBar = document.querySelector("mobile-search-bar");

const searchInput = document.querySelector("[data-search]");
const mosearchInput = document.querySelector("[data-search]");

const resultBox = document.querySelector(".result-box");
const resultBox2 = document.querySelector(".result-box-2");


let suggestions = [
    'Hypnosis porn trance',
    'BrainBreak 1',
    'Loopy Goon',
    'Goon Hypnosis',
    'Average HFO enjoyer',
    'Jackpot HFO Hypno JOI',
    'The Greatest Goon Video Ever Made',
    'Big titty addiction',
    'The Greatest Goon Video Ever Made',
    'Lust angels',
    'BIG TITTY LOVER JOI',
    'Hypno Mindfuck Goon JOI',
    'Brainwashed to gooon'
];
let result = [];
function display(result) {

    const content = result.map((listItem) => {
        // Manually build the URL for the video
        const url = `/Video/Watch?title=${listItem.replace(/ /g, '-')}`;
        return `<a href="${url}" style="color: white"><li>${listItem}</li></a>`;
    });

    resultBox.innerHTML = "<ul>" + content.join('') + "</ul>";
    const listItems = resultBox.querySelectorAll('li');

    // Add event listeners after setting innerHTML
    listItems.forEach((li) => {
        li.addEventListener('click', function () {
            selectInput(this);
        });
    });
}

function selectInput(list) {
    console.log("search result item clicked");
     resultBox.innerHTML = '';
}


searchBar.addEventListener("focus", () => {
    searchCue.classList.add("search-cue");
});
searchBar.addEventListener("focusout", () => {
    searchCue.classList.remove("search-cue");
});

searchInput.addEventListener("input", e => {
    const value = e.target.value;
    console.log(value);
    result = suggestions.filter((keyword) => {
        return keyword.toLowerCase().includes(value.toLowerCase());
    });
    console.log(result);

   // Call function to display
    display(result);
    if (!result.length) {
        resultBox.innerHTML = '';
    }
});


mobileSearchBar.addEventListener("focus", () => {
    searchCue.classList.add("search-cue");
});
mobileSearchBar.addEventListener("focusout", () => {
    searchCue.classList.remove("search-cue");
});

mosearchInput.addEventListener("input", e => {
    const value = e.target.value;
    console.log(value);
    result = suggestions.filter((keyword) => {
        return keyword.toLowerCase().includes(value.toLowerCase());
    });
    console.log(result);
    display(result);
    if (!result.length) {
        resultBox2.innerHTML = '';
    }
});s