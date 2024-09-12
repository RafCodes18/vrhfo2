const searchCue = document.querySelector('.btn-outline-primary');
const searchBar = document.querySelector('.custom-search');

searchBar.addEventListener("focus", () => {
    searchCue.classList.add("search-cue");
});
searchBar.addEventListener("focusout", () => {
    searchCue.classList.remove("search-cue");
    resultBox.innerHTML = '';
});

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

function display(result) {
    const content = result.map((list) => {
        return "<li onclick=selectInput(this)>" + list + "</li>";
    });

    resultBox.innerHTML = "<ul>" + content.join('') + "</ul>";
}

function selectInput(list) {
    searchInput.value = list.innerHTML;
    resultBox.innerHTML = '';
}