async function loadTasks(keyword = "") {
    const endpoint = keyword ? `Task/search?name=${keyword}` : "Task";
    const res = await apiRequest(endpoint);
    document.getElementById('taskList').innerHTML = res.data.map(t => `
        <tr>
            <td>${t.title}</td>
            <td>${t.userName}</td>
            <td><span class="badge">${t.status}</span></td>
            <td>
                <button class="btn btn-outline" onclick="location.href='edit-task.html?id=${t.taskId}'">Edit</button>
                <button class="btn btn-outline" style="color:red" onclick="delTask(${t.taskId})">Delete</button>
            </td>
        </tr>
    `).join('');
}

async function delTask(id) {
    if (confirm("Delete?")) {
        await apiRequest(`Task/${id}`, "DELETE");
        loadTasks();
    }
}

function search() { loadTasks(document.getElementById('search').value); }
loadTasks();