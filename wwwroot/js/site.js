// Image preview logic
function previewImage(event)
{
    var input = event.target;
    var preview = document.getElementById('image-Preview');
    var previewSection = document.getElementById('image-preview-section');

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            preview.src = e.target.result;
            previewSection.style.display = 'block';
            previewSection.style.objectFit = 'contain';
        };

        reader.readAsDataURL(input.files[0]);
    }
}

// Popup show logic
function togglePopup(event)
{
    console.log("run");
    const element = document.querySelector(".js-popup-background");
    if (element.classList.contains("collapse")) {
        // Toggle visible
        element.classList.remove("collapse");
    }
    else
    {
        element.classList.add("collapse");
    }
}

// Prevents the popup from unshowing when the user clicks on popup elements
function stopClickPropagation(event) {
    event.stopPropagation();
}