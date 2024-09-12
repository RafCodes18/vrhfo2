const searchCue = document.querySelector('.btn-outline-primary');
const searchBar = document.querySelector('.custom-search');

const searchInput = document.querySelector("[data-search]");
const resultBox = document.querySelector(".result-box");

let suggestions = [
    'Gooning',
    'Binaural Beats',
    'HFO',
    'Hands free orgasm',
    'Brainwashing',
    'Porn addict',
    'Porn addict brainwashing program',
    'Porn worship',
    'The greatest goon video ever made',
    'Big titty JOI',
    'Average HFO enjoyer',
    'Goon Binaural Beats'
];
let result = [];
function display(result) {

    const content = result.map((list) => {
        return "<li>" + list + "</li>";
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
    searchInput.value = list.innerHTML;
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
    display(result);
    if (!result.length) {
        resultBox.innerHTML = '';
    }
});