async function initForm() {
    // 1. Load the Users into the dropdown first
    const uRes = await apiRequest("User");
    const dropdown = document.getElementById('userDropdown');

    uRes.data.forEach(u => {
        const opt = document.createElement('option');
        opt.value = u.userId;
        opt.innerText = u.userName;
        dropdown.appendChild(opt);
    });

    // 2. Check if we are in "Edit" mode by looking for an ID in the URL
    const urlParams = new URLSearchParams(window.location.search);
    const taskId = urlParams.get('id');

    if (taskId) {
        // Fetch existing task data from API
        const tRes = await apiRequest(`Task/${taskId}`);
        if (tRes.success) {
            const task = tRes.data;
            // Fill the form fields with existing data
            document.getElementById('title').value = task.title;
            document.getElementById('desc').value = task.description;
            document.getElementById('status').value = task.status;
            document.getElementById('userDropdown').value = task.userId;
        }
    }
}

async function saveTask() {
    const urlParams = new URLSearchParams(window.location.search);
    const taskId = urlParams.get('id');

    // Create the DTO object
    const dto = {
        title: document.getElementById('title').value,
        description: document.getElementById('desc').value,
        status: document.getElementById('status').value,
        userId: parseInt(document.getElementById('userDropdown').value)
    };

    // Validation: Title and User are mandatory
    if (!dto.title || !dto.userId) {
        alert("Please provide a title and assign a user.");
        return;
    }

    let result;
    if (taskId) {
        // If ID exists, call PUT (Update)
        result = await apiRequest(`Task/${taskId}`, "PUT", dto);
    } else {
        // If no ID, call POST (Create)
        result = await apiRequest("Task", "POST", dto);
    }

    if (result.success) {
        alert("Success!");
        window.location.href = "tasks.html";
    } else {
        alert("Error: " + result.message);
    }
}

// Run the initialization when page loads
document.addEventListener('DOMContentLoaded', initForm);