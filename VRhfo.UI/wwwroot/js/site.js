const searchCue = document.querySelector('.btn-outline-primary');
const searchCue2 = document.querySelector('.btn-outline-primary2');
const searchBar = document.querySelector('.custom-search');
const mobileSearchBar = document.querySelector(".mobile-search-bar");

const searchInput = document.querySelector("[data-search]");
const mosearchInput = document.querySelector("[data-search2]");

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
    resultBox2.innerHTML = "<ul>" + content.join('') + "</ul>";
    const listItems = resultBox.querySelectorAll('li');
    const listItems2 = resultBox2.querySelectorAll('li');

    // Add event listeners after setting innerHTML
    listItems.forEach((li) => {
        li.addEventListener('click', function () {
            selectInput(this);
        });
    });
    // Add event listeners after setting innerHTML
    listItems2.forEach((li2) => {
        li2.addEventListener('click', function () {
            selectInput(this);
        });
    })
}

function selectInput(list) {
    console.log("search result item clicked");
     resultBox.innerHTML = '';
     resultBox2.innerHTML = '';
}


searchBar.addEventListener("focus", () => {
    searchCue.classList.add("search-cue");
});
searchBar.addEventListener("focusout", () => {
    searchCue.classList.remove("search-cue");
});



mobileSearchBar.addEventListener("focus", () => {
    searchCue2.classList.add("search-cue");
});
mobileSearchBar.addEventListener("focusout", () => {
    searchCue2.classList.remove("search-cue");
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
}); 


// Streak logic and modal handling
function getLocalDateString() {
    return new Date().toLocaleDateString();
}

function showStreakModal(streak) {
    const modal = document.getElementById("streakModal");
    const title = document.getElementById("streakTitle");
    const desc = document.getElementById("streakDesc");

    title.textContent = `Streak: ${streak} Day${streak > 1 ? 's' : ''}`;
    if (streak === 1) {
        desc.textContent = "Welcome back. You're starting a new streak. Come back tomorrow to keep it going!";
    } else if (streak === 3) {
        desc.textContent = "Day 3! You're heating up. A reward may be unlocked soon…";
    } else if (streak === 5) {
        desc.textContent = "💎 Day 5. You're becoming elite. Keep going!";
    } else {
        desc.textContent = "Keep it up! Come back tomorrow to build your streak. Rewards may be coming… 👀";
    }

    modal.style.display = "flex";
}

function closeStreakModal() {
    document.getElementById("streakModal").style.display = "none";
}

(function handleStreakLogic() {
    const today = getLocalDateString();
    const lastVisit = localStorage.getItem("lastVisitDate");
    const lastModal = localStorage.getItem("lastModalShownDate");
    let streak = parseInt(localStorage.getItem("streakCount") || "0");
    let longest = parseInt(localStorage.getItem("longestStreak") || "0");

    const yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    const yesterdayStr = yesterday.toLocaleDateString();

    if (lastVisit === today) return;

    if (lastVisit === yesterdayStr) {
        streak += 1;
    } else {
        if (streak > longest) {
            localStorage.setItem("longestStreak", streak);
        }
        streak = 1;
    }

    // Save values
    localStorage.setItem("streakCount", streak);
    localStorage.setItem("lastVisitDate", today);

    if (lastModal  !== today) {
        localStorage.setItem("lastModalShownDate", today);
        showStreakModal(streak);
    }
})();