// Import des modules HttpClient et Injectable depuis Angular
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

// TODO : GESTION DES MDPS !!!!!!
// Définition de l'interface User représentant la structure d'un utilisateur
export interface User {
    id: number;
    username: string;
    password: string | undefined;
}

// Décorateur Injectable indiquant que le service peut être injecté au niveau racine
@Injectable({
    providedIn: "root",
})
// Définition de la classe du service UserService
export class UserService {
    // Propriété user qui représente l'utilisateur actuellement connecté
    user: undefined | User;

    // Constructeur du service avec injection du module HttpClient
    constructor(private http: HttpClient) {}

    // Méthode pour récupérer un utilisateur par son ID
    getUserById(id: string) {
        return this.http.get<User>(`http://localhost:5111/users/${id}`);
    }
    // Méthode de connexion (login) prenant un nom d'utilisateur en paramètre
    login(username: string) {
        // Affichage dans la console du message de connexion avec le nom d'utilisateur
        console.log("login : " + username);

        // Utilisation du module HttpClient pour envoyer une requête POST vers l'API
        this.http
            .post("http://localhost:5111/users", { username })
            // Abonnement pour recevoir la réponse de l'API
            .subscribe((user: any) => {
                // Affichage dans la console des informations de l'utilisateur reçues
                console.log("user : ", user);

                // Mise à jour de la propriété user du service avec les données de l'utilisateur
                this.user = user;
            });
    }

    // Méthode de déconnexion (logout)
    logout() {
        // Réinitialisation de la propriété user à undefined
        this.user = undefined;
    }


}
