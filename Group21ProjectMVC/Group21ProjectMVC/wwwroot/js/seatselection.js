function SelectSeat(id) {
    if (!id.checked) {
        var selectedtab = [...document.getElementById('flightTabs').children].find(
            e =>
                e.classList.contains("active")
        );
        if (selectedtab.id === 'DepartureTab') {
            var selectedList = [...document.getElementById('DepartureList').children].find(
                e =>
                    e.classList.contains("active")
            );
            var x = document.getElementById('DepartureFlightSeats' + selectedList.id.replace("DepartureList", ""));
            if (x.value === "") {
                x.value = (id.id.replace('Departure', ''));
                selectedList.querySelector('span').innerText = x.value;
                id.checked = true;
            } else {
                document.getElementById('Departure' + x.value).checked = false;
                x.value = (id.id.replace('Departure', ''));
                selectedList.querySelector('span').innerText = x.value;
                id.checked = true;
            }
            if (selectedList.nextElementSibling != null) {
                SelectListItem(selectedList.nextElementSibling);
            } else {
                SelectListItem(selectedList.parentElement.firstElementChild);
            }
        } else if (selectedtab.id === 'ReturnTab') {
            var selectedList = [...document.getElementById('ReturnList').children].find(
                e =>
                    e.classList.contains("active")
            );
            var x = document.getElementById('ReturnFlightSeats' + selectedList.id.replace("ReturnList", ""));
            console.log(x);
            if (x.value === "") {
                x.value = (id.id.replace('Return', ''));
                selectedList.querySelector('span').innerText = x.value;
                id.checked = true;
            } else {
                document.getElementById('Return' + x.value).checked = false;
                x.value = (id.id.replace('Return', ''));
                selectedList.querySelector('span').innerText = x.value;
                id.checked = true;
            }
            if (selectedList.nextElementSibling != null) {
                SelectListItem(selectedList.nextElementSibling);
            } else {
                SelectListItem(selectedList.parentElement.firstElementChild);
            }
        }
    }
}

function SelectListItem(id) {
    var selectedList = [...document.getElementById(id.id.slice(0, -1)).children].find(
        e =>
            e.classList.contains("active")
    );
    selectedList.classList.remove("active");
    id.classList.add("active");
}