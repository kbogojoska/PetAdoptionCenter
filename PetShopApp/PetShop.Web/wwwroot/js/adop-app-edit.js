const animalTypeMapping = {
    0: 'DOG',
    1: 'CAT',
    2: 'RABBIT',
    3: 'HAMSTER',
    4: 'PARROT',
    5: 'MOUSE',
    6: 'RAT',
    7: 'FISH'
};


function updatePetInfo(selectedPetId) {
    console.log('Selected Pet ID:', selectedPetId);

    const selectedPet = pets.find(pet => pet.Id == selectedPetId);

    if (selectedPet) {
        console.log('Selected Pet:', selectedPet);

        document.getElementById('petImage').src = selectedPet.ImageURL || '';
        document.getElementById('petName').textContent = selectedPet.Name || '';
        document.getElementById('petType').textContent = animalTypeMapping[selectedPet.Type] || 'Unknown';
        document.getElementById('petWeight').textContent = selectedPet.Weight ? `${selectedPet.Weight} kg` : '';
        document.getElementById('petAge').textContent = selectedPet.Age ? `${selectedPet.Age} years` : '';
        document.getElementById('petBreed').textContent = selectedPet.Breed || '';
        document.getElementById('petPrice').textContent = selectedPet.PriceForAdoption ? `$${selectedPet.PriceForAdoption}` : '';

        document.getElementById('petInfoCard').style.display = 'block';
    } else {
        console.log('No pet found with ID:', selectedPetId);
        document.getElementById('petInfoCard').style.display = 'none';
    }
}
