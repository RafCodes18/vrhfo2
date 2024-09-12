const playPauseBtn = document.querySelector(".play-pause-btn");
const video = document.querySelector('video');
const videoContainer = document.querySelector(".video-container");
const theaterBtn = document.querySelector(".theater-btn");
const fullscreenBtn = document.querySelector('.full-screen-btn');
const mainElement = document.querySelector('main');
const muteBtn = document.querySelector('.mute-btn');
const volumeSlider = document.querySelector('.volume-slider');
const currentTime = document.querySelector('.current-time');
const totalTime = document.querySelector('.total-time');
const timelineContainer = document.querySelector('.timeline-container');
const controlsContainer = document.querySelector(".video-controls-container");
const currentUserIdd = document.getElementById("curr-user").textContent;
const currVidId = document.getElementById("curr-vid").textContent;


let hideControlsTimeout;

// watch stats
let _watchDuration = 0;
let intervalId;

function SkipForward() {
    if (video) {
        video.currentTime = Math.min(video.currentTime + 10, video.duration); // Ensure it doesn't exceed duration
    }
}

function SkipBackward() {
    if (video) {
        video.currentTime = Math.max(video.currentTime - 10, 0); // Ensure it doesn't go below 0
    }
}

function hideControls() {
    hideControlsTimeout = setTimeout(() => {
        controlsContainer.style.opacity = '0';
    }, 1000); // Hide controls 3 seconds after mouse leaves
}

function showControls() {
    clearTimeout(hideControlsTimeout);
    controlsContainer.style.opacity = '1';
}

// Show controls when moving the mouse in full-screen mode
videoContainer.addEventListener('mousemove', () => {
    if (isFullScreen()) {
        showControls();
        hideControls(); // Restart the hide countdown when mouse moves
    }
});

// Start hiding controls 3 seconds after mouse leaves the video container in full-screen mode
videoContainer.addEventListener('mouseleave', () => {
    if (isFullScreen()) {
        hideControls();
    }
});

// Function to check if the video is in full-screen mode
function isFullScreen() {
    return document.fullscreenElement || document.webkitFullscreenElement || document.mozFullScreenElement;
}

// Full-screen change event to show controls when entering full-screen mode
document.addEventListener('fullscreenchange', () => {
    if (isFullScreen()) {
        showControls();
        hideControls(); // Start the hide countdown when entering full-screen mode
    } else {
        clearTimeout(hideControlsTimeout); // Stop the countdown when exiting full-screen mode
        controlsContainer.style.opacity = '1'; // Ensure controls are visible
    }
});


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
        case "m":
            toggleMute();
        case "arrowright":
            SkipForward();
            break;
        case "arrowleft":
            SkipBackward();
            break;
    }
});



timelineContainer.addEventListener("mousemove", handleTimelineUpdate);
timelineContainer.addEventListener("mousedown", toggleScrubbing);
document.addEventListener("mouseup", e => {
    if (isScrubbing) toggleScrubbing(e);
})
document.addEventListener("mousemove", e => {
    if (isScrubbing) handleTimelineUpdate(e);
})

let isScrubbing = false;
let wasPaused
function toggleScrubbing(e) {
    const rect = timelineContainer.getBoundingClientRect();
    const percent = Math.min(Math.max(0, e.x - rect.x), rect.width) / rect.width
    isScrubbing = (e.buttons & 1) === 1
    videoContainer.classList.toggle("scrubbing", isScrubbing)
    if (isScrubbing) {
        wasPaused = video.paused;
        video.pause();
    } else {
        video.currentTime = percent * video.duration;
        if (!wasPaused) video.play();
        
    }
    handleTimelineUpdate(e);
}
function handleTimelineUpdate(e) {
    const rect = timelineContainer.getBoundingClientRect();
    const percent = Math.min(Math.max(0, e.x - rect.x), rect.width) / rect.width;
    timelineContainer.style.setProperty("--preview-position", percent);

    if (isScrubbing) {
        e.preventDefault();
        timelineContainer.style.setProperty("--progress-position", percent);
    }
}

//duration
video.addEventListener("loadeddata", () => {
    totalTime.textContent = formatDuration(video.duration);
});
video.addEventListener("timeupdate", () => {
    const percent = video.currentTime / video.duration;
    currentTime.textContent = formatDuration(video.currentTime);
    timelineContainer.style.setProperty("--progress-position", percent);

})

const leadingZeroFormatter = new Intl.NumberFormat(undefined, {
    minimumIntegerDigits: 2,
})
function formatDuration(time) {
    const seconds = Math.floor(time % 60)
    const minutes = Math.floor(time / 60) % 60
    const hours = Math.floor(time / 3600)

    if (hours === 0) {
        return `${minutes}:${leadingZeroFormatter.format(seconds)}`
    } else {
        return `${hours}:${leadingZeroFormatter.format(
            minutes
        )}:${leadingZeroFormatter.format(seconds)}`
    }
}


//volume
muteBtn.addEventListener("click", toggleMute);
volumeSlider.addEventListener("input", e => {
    video.volume = e.target.value;
    video.muted = e.target.value === 0;
})
function toggleMute() {
    video.muted = !video.muted
    if (video.muted) {
        volumeSlider.style.background = 'darkgrey';
    }
    else {
        volumeSlider.style.background = `linear-gradient(to right, deeppink ${value}%, darkgray ${value}%)`;
    }
}
volumeSlider.addEventListener('input', function () {
    const value = this.value * 100; // Get value as percentage
    this.style.background = `linear-gradient(to right, deeppink ${value}%, darkgray ${value}%)`;
});



video.addEventListener("volumechange", () => {

    volumeSlider.value = video.volume
    let volumeLevel
    if (video.muted || video.volume === 0) {
        volumeSlider.value = 0
        volumeLevel = "muted"
    } else if (video.volume >= 0.5) {
        volumeLevel = "high"
    } else {
        volumeLevel = "low"
    }

    videoContainer.dataset.volumeLevel = volumeLevel


})

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
    console.log("play detected");
    intervalId = setInterval(updateWatchDuration, 5000); // Every 5 seconds
});

video.addEventListener("pause", () => {
    videoContainer.classList.add("paused");
    clearInterval(intervalId);


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









// Function to update watch duration
function updateWatchDuration() {
    _watchDuration = video.currentTime;
    localStorage.setItem('watchDuration', _watchDuration);
}


// Send final update when video ends
video.addEventListener('ended', function () {
    sendProgressUpdate();
});

// Send update when user leaves the page
window.addEventListener('beforeunload', function () {
    sendProgressUpdate();
});

// Helper function to format duration as TimeSpan string
function formatDuration2(seconds) {
    const hours = Math.floor(seconds / 3600);
    const minutes = Math.floor((seconds % 3600) / 60);
    const remainingSeconds = Math.floor(seconds % 60);
    return `${hours}:${minutes.toString().padStart(2, '0')}:${remainingSeconds.toString().padStart(2, '0')}`;
}


// Function to send progress update
// Function to send progress update
async function sendProgressUpdate() {
    const completed = currentTime === _watchDuration;

    if (!currentUserIdd || currentUserIdd === '00000000-0000-0000-0000-000000000000') {
        console.log('User not logged in, progress not tracked');
        return;
    } else {
        console.log("sending stats...");
        try {
            const response = await fetch('/Video/UpdateWatchProgress', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    userId: currentUserIdd,
                    videoId: currVidId, // Ensure this variable is correctly set
                    watchDuration: formatDuration2(_watchDuration),
                    completed: completed
                })
            });

            const data = await response.json();

            if (response.ok && data.success) {
                console.log('Progress updated successfully');
            } else {
                console.log('Progress update failed');
                console.log(data.message);
            }
        } catch (error) {
            console.error('Error occurred while updating progress:', error);
        }
    }
}



