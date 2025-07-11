/*Categories*/

books = db.categories.insertOne({
    "name" : {
        "be": "Кнігі",
        "ru": "Книги"
    }
})

documents = db.categories.insertOne({
    "name" : {
        "be": "Дакументы",
        "ru": "Документы"
    }
})

gallery = db.categories.insertOne({
    "name" : {
        "be": "Галерэя",
        "ru": "Галерея"
    }
})

social = db.categories.insertOne({
    "name" : {
        "be": "Свецкае",
        "ru": "Светское"
    },
    "parentId" : books.insertedId
})

fairyTales = db.categories.insertOne({
    "name" : {
        "be": "Казкі",
        "ru": "Сказки"
    },
    "parentId" : books.insertedId
})

religious = db.categories.insertOne({
    "name" : {
        "be": "Рэлігія",
        "ru": "Религия"
    },
    "parentId" : books.insertedId
})

law = db.categories.insertOne({
    "name" : {
        "be": "Права",
        "ru": "Право"
    },
    "parentId" : documents.insertedId
})

letters = db.categories.insertOne({
    "name" : {
        "be": "Лісты",
        "ru": "Письма"
    },
    "parentId" : documents.insertedId
})

map = db.categories.insertOne({
    "name" : {
        "be": "Карты",
        "ru": "Карты"
    },
    "parentId" : gallery.insertedId
})

other = db.categories.insertOne({
    "name" : {
        "be": "Іншае",
        "ru": "Другое"
    },
    "parentId" : gallery.insertedId
})

/*Languages*/

churchSlavonic = db.languages.insertOne({
    "name" : {
        "be": "Царкоўнаславянская",
        "ru": "Церковнославянский"
    }
}) 

oldEastSlavic = db.languages.insertOne({
    "name" : {
        "be": "Старажытнаруская",
        "ru": "Древнерусский"
    }
})

ruthenian = db.languages.insertOne({
    "name" : {
        "be": "Старабеларуская",
        "ru": "Старобелорусский"
    }
})

russian = db.languages.insertOne({
    "name" : {
        "be": "Руская",
        "ru": "Русский"
    }
})

belarussian = db.languages.insertOne({
    "name" : {
        "be": "Беларуская",
        "ru": "Белорусский"
    }
})