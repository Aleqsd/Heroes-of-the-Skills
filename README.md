# Heroes of the Skills

## Introduction

Une armée de monstres souhaite détruire le Nexus. Pour les en empêcher, quatre héros légendaires s’allient et défendent le fort jusqu’à leur dernier souffle.

## TeamSkills

![Alt text](Doc/Team.png?raw=true "TeamSkills")

## Description

L’objectif est de mettre en scène quatre héros défendant chacun un côté du fort (Nord, Sud, Est, Ouest).
Chaque héro aura des capacités différentes (Soigneur, Tank, DPS défensif, DPS offensif, etc…).
Le jeu fonctionne par vagues d’ennemis où les héros devront tuer tous les ennemis en laissant les ennemis faire subir le moins de dégâts possible au fort. À la fin de chaque vague, des items apparaissent et les héros ont quelques secondes pour se les répartir équitablement avant l’arrivée de la prochaine vague.

Si un héros meurt, il réapparaît à la fin d’une vague. Toutefois, si les quatre héros meurent durant une même vague d’ennemis, la partie est terminée.

Enfin, si les héros parviennent à se débarrasser de toutes les vagues d’ennemis, le boss de fin de jeu apparaît et il devra être vaincu pour finir la partie.

## Fonctionnalités

- Jouable en Solo et Multiplayer
- Choix possible entre 4 classes (Electricité / Feu / Air / Soins)
- Attaques simples et attaques spéciales
- Spawn de monstres
- Intelligence Artificielle des monstres
- Système de round
- Boucle de jeu
- FX / Particules

A implanter :

- Evolution des spells après les rounds
- Map / UI
- Fix d'animations en multi
- Optimisation de latence multi

## Game loop

![Alt text](Doc/GameLoop.png?raw=true "Game loop structure")

