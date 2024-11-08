﻿using System;
using System.Collections.Generic;

namespace CercleRoyalEscrimeTournaisien
{
    [Serializable]
    public class Oscar_NosCoursTireurSelectionneModel : NosCoursTireurSelectionneModel
    {
        private Tireur _tireurSelectionne { get; set; }
        public Oscar_NosCoursTireurSelectionneModel(Tireur tireurSelectionne)  : base()
        {
            this._tireurSelectionne = tireurSelectionne;
        }
        public List<RemarqueParDate> RemarquesParDateTireur
        {
            get
            {
                List<RemarqueParDate> remarquesParDateTireur = new List<RemarqueParDate>() { };
                remarquesParDateTireur.AddRange(new List<RemarqueParDate>()
                {
                    new RemarqueParDate()
                    {
                        DateRemarque = new DateTime(2024, 10, 20),
                        Arme = TypeArme.Sabre,
                        Tireur = new Tireur()
                        {
                            UserNameIndex = this._tireurSelectionne.UserNameIndex,
                        },
                        RemarquesData = new List<RemarqueData>()
                            {
                                new RemarqueData()
                                {
                                    //HasPointPositif=true,
                                    PointNégatif="Généralement, lors que tu avances vers ton adversaire tu attaques du coup tu ne dois pas avoir peur d'être touché vu que tu es à l'attaque. Donc va jusqu'à la touche vu que tu as la priorité. Dans cette séquence vidéo, tu vas vers Raedwald en l'attaquant du coup pas besoin de parade qui doit se faire avec le bras fléchi. non. Va le toucher et ne pense pas à te défendre. Première règle au sabre comme au fleuret, celui qui est à l'attaque, sans qu'il y ait eu de parade, gagne le point.",
                                    HasUrlVideo=true,
                                    UrlVideo = "/Videos/20-10-2024_Oscar.avi",
                                },
                                new RemarqueData()
                                {
                                 //   HasPointPositif=false,
                                    PointNégatif="Quand tu tires contre un gaucher qui ne protège pas correctement sa garde, comme ici sur la vidéo, essaie d'aller par la droite tout en essayant d'aller auparavant à gauche. Je vais à gauche d'abord (ici en quarte) et puis je finis en tierce à droite. Raedwald étant mal en garde, tu le toucheras par la droite en mettant ton pouce à 3h et en te décalant sur la piste sur le côté droit.",
                                    HasUrlVideo=true,
                                    UrlVideo = "/Videos/20-10-2024_Oscar_2.avi",
                                },

                            }
                    }
                });

                return remarquesParDateTireur;
            }
        }
      
    }
}