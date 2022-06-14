// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*let arr = [1, "2", [1, 2, 3]];

arr.unshift(5);
arr.shift();
console.log(arr);

const animals = [
    { name: "Garfield", species: "cat", class :{ name: "mamalia" } },
    { name: "Nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "Tom", species: "cat", class: { name: "mamalia" } },
    { name: "Bruno", species: "fish", class: { name: "ivertebrata" } },
    { name: "Carlo", species: "cat", class: { name: "mamalia" } }
]


let onlyCat=[];
let onlyFish = [];

for (let i = 0; i < animals.length; i++) {
    if (animals[i].species === "cat") {
        onlyCat.push(animals[i])
    } else {
        animals[i].class.name = "non-mammalia"
    }
}

console.log(onlyCat);
console.log(animals);*/

/*
$("h1").html("testing diubah dari jquery");*/

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
}).done((result) => {
    console.log(result.results);
    let text = "";
    $.each(result.results, function (key, value) {
        text += `<tr>
                    <td>${key+1}</td>
                    <td>${value.name}</td>
                    <td><button type="button" onClick="pokemonDetail('${value.url}')" class="btn btn-primary" data-toggle="modal" data-target="#modalDetail">Detail</button></td>
                </tr>`;

        
    });

    
    $("#tabel-pokemon").html(text);

}).fail((error) => {
    console.log(error);
})

function pokemonDetail(url) {
    $.ajax({
        url: url
    }).done((res) => {
        $(".pokemon-name").html(res.name);
        $(".pokemon-img").attr("src", res.sprites.other.home.front_default)
        let pokemon_type = res.types;
        let txt_type = ""
        for (let i = 0; i < pokemon_type.length; i++) {
            txt_type += `<span class="badge badge-pill badge-primary">${pokemon_type[i].type.name}</span>`
        }
        $(".pokemon-type").html(txt_type);


        $("#hp-value").css("width", res.stats[0].base_stat + "%");
        $("#hp-value").html(res.stats[0].base_stat);
        $("#attack-value").css("width", res.stats[1].base_stat + "%");
        $("#attack-value").html(res.stats[1].base_stat);
        $("#defense-value").css("width", res.stats[2].base_stat + "%");
        $("#defense-value").html(res.stats[2].base_stat);
        $("#sp-attack-value").css("width", res.stats[3].base_stat + "%");
        $("#sp-attack-value").html(res.stats[3].base_stat);
        $("#sp-defense-value").css("width", res.stats[4].base_stat + "%");
        $("#sp-defense-value").html(res.stats[4].base_stat);
        $("#speed-value").css("width", res.stats[5].base_stat + "%");
        $("#speed-value").html(res.stats[5].base_stat);

        let txt_abilities = "";
        let pokemon_ability = res.abilities;
        for (let i = 0; i < pokemon_ability.length; i++) {
            txt_abilities += `<div class="row ability">${pokemon_ability[i].ability.name}</div>`
        }

        $("#abilities").html(txt_abilities);

        encounter_pokemon(res.location_area_encounters);
        
    })
    console.log(url);
}

function encounter_pokemon(url) {
    $.ajax({
        url: url
    }).done((res) => {
        let encounters = "";
        for (let i = 0; i < res.length; i++) {
            encounters += `<div class="row encounter">${res[i].location_area.name}</div>`
        }
        $("#encounters").html(encounters);
    })
}