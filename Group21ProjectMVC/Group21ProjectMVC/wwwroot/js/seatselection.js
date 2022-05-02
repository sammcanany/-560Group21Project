function SelectSeat(id) {
    if (!id.checked) {
        var selectedtab = [...document.getElementById('flightTabs').children].find(
            e =>
                e.classList.contains("active")
        );
        if (selectedtab.id === 'DepartureTab') {
            var selectedList = [...document.getElementById('DepartureList').children].find(
                e =>
                    e.classList.contains("selected")
            );
            var x = document.getElementById('DepartureFlightSeats' + selectedList.id.replace("DepartureList", ""));
            if (x.value != 0) {
                document.getElementById('Departure' + x.value).checked = false;
            }
            x.value = (id.id.replace('Departure', ''));
            [...selectedList.firstElementChild.children].find(
                e =>
                    e.classList.contains("seatNumber")
            ).innerText = x.value;
            id.checked = true;
            console.log(id);
            SelectListItem(selectedList);
        } else if (selectedtab.id === 'ReturnTab') {
            var selectedList = [...document.getElementById('ReturnList').children].find(
                e =>
                    e.classList.contains("selected")
            );
            var x = document.getElementById('ReturnFlightSeats' + selectedList.id.replace("ReturnList", ""));
            if (x.value != 0) {
                document.getElementById('Return' + x.value).checked = false;
            }
            x.value = (id.id.replace('Return', ''));
            [...selectedList.firstElementChild.children].find(
                e =>
                    e.classList.contains("seatNumber")
            ).innerText = x.value;
            id.checked = true;
            console.log(id);
            SelectListItem(selectedList);
        }
    }
}

function SelectListItem(id) {
    var selectedList = [...document.getElementById(id.id.slice(0, -1)).children].find(
        e =>
            e.classList.contains("selected")
    );
    selectedList.classList.remove("selected");
    if (selectedList.nextElementSibling != null) {
        selectedList.nextElementSibling.classList.add("selected");
    } else {
        selectedList.parentElement.firstElementChild.classList.add("selected");
    }
}