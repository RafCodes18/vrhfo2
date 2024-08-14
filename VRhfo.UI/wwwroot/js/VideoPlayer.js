const playPauseBtn = document.querySelector(".play-pause-btn");
const video = document.querySelector('video');
const videoContainer = document.querySelector(".video-container");


document.addEventListener("keydown", e => {
        //STOP SCROLL ON SPACEBAR
        if (e.keyCode == 32 && e.target == document.body) {
            e.preventDefault();
        }
   
    switch (e.key.toLowerCase()) {
        case "k":
        case " ":
            togglePlay()
            break;
    }
});

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