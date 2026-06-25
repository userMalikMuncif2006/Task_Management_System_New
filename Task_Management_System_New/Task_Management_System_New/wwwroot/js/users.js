async function loadUsers() {
    const res = await apiRequest("User");
    const tbody = document.getElementById('userList');
    tbody.innerHTML = res.data.map(u => `
        <tr>
            <td>${u.userId}</td>
            <td>${u.userName}</td>
            <td>${u.email}</td>
            <td><button class="btn btn-outline" onclick="location.href='user-detail.html?id=${u.userId}'">View Profile</button></td>
        </tr>
    `).join('');
}

async function addUser() {
    const dto = { userName: document.getElementById('uName').value, email: document.getElementById('uEmail').value };
    const res = await apiRequest("User", "POST", dto);
    if (res.success) location.reload();
}
loadUsers();