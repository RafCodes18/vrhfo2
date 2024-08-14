const playPauseBtn = document.querySelector(".play-pause-btn");
const video = document.querySelector('video');
const videoContainer = document.querySelector(".video-container");
const theaterBtn = document.querySelector(".theater-btn");
const fullscreenBtn = document.querySelector('.full-screen-btn');
const mainElement = document.querySelector('main');

document.addEventListener("keydown", e => {
    const tagName = document.activeElement.tagName.toLowerCase();
    if (tagName == "input") return;

        //STOP SCROLL ON SPACEBAR
        if (e.keyCode == 32 && e.target == document.body) {
            e.preventDefault();
        }
   
    switch (e.key.toLowerCase()) {
        case " ":
            if(tagName === "button") return
        case "k":
            togglePlay()
            break;
        case "f":
            toggleFullscreenMode();
            break;
        case "t":
            toggleTheaterMode();
            break;
    }
});

//VIEW MODES
theaterBtn.addEventListener('click', toggleTheaterMode);
fullscreenBtn.addEventListener('click', toggleFullscreenMode);

function toggleTheaterMode() {
    videoContainer.classList.toggle("theater");
    updateFlexDirection();
}

function toggleFullscreenMode() {
    if (document.fullscreenElement == null) {
        videoContainer.requestFullscreen();
    } else {
        document.exitFullscreen();
    }
}

document.addEventListener('fullscreenchange', () => {
    videoContainer.classList.toggle("full-screen", document.fullscreenElement);
})

playPauseBtn.addEventListener('click', () => {
    togglePlay();
});
video.addEventListener('click', () => {
    togglePlay();
    console.log("video clicked");
});


video.addEventListener("play", () => {
    videoContainer.classList.remove("paused");
});

video.addEventListener("pause", () => {
    videoContainer.classList.add("paused");
});


function togglePlay() {
    video.paused ? video.play() : video.pause();
}




//REMOVE PAGE RIGHT IN THEATER MODE
// Select the main element whose flex direction needs to be changed

// Function to update the flex direction based on the presence of the 'theater' class
function updateFlexDirection() {
    if (videoContainer.classList.contains('theater')) {
        mainElement.style.flexDirection = 'column';
        mainElement.style.classList.add('full-vid');
    } else {
        mainElement.style.flexDirection = ''; // Revert to default
    }
}

// Create a MutationObserver to watch for changes in the class attribute of videoContainer
const observer = new MutationObserver(() => {
    updateFlexDirection();
});

// Start observing the video container for attribute changes
observer.observe(videoContainer, { attributes: true, attributeFilter: ['class'] });

// Initial check in case the class is already present
updateFlexDirection();