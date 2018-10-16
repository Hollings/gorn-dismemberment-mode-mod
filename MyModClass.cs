using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*______________________Info_________________________ 
 * Template provided by: MemeMan (MemeLoader creator).*
 *                                                    *
 * Supports: MemeLoader V0.5.4                        * 
 * __________________________________________________ *
 * TIP: To open a region, click the plus symbol!    
 */

#region Error help

/*Q = Question
 *A = Answer
 *O = Optional
 *I = Additional Information
 * - - - - - - - - - - - - - 
 * -Q: It says I'm missing an assembly reference?-
 * ==============================================
 * A: View>Solution Explorer>References>Right-click>Add>Clear all(if any show up)(Right-click one and clear all)>Browse>Project root>Plugins>Select All.
 * 
 * -Q: My mod won't load!- 
 * ========================
 *  A: Did you remove Init()? If not, everything should work, it'll be your code, double check!
 *  I: I keep dlSpy(.dll deassembler) open so I can see the source to understand what I'm modifying.
 *  
 *  -Q: I accidentally broke the game, help!-
 *  =========================================
 *  A: Delete: GORN_Data>Managed>Assembly-CSharp.dll and the most recent mod you broke it with.
 *  O: Verify file integrity, launching the game will start this automatically.
 */

#endregion

namespace MyModNameSpace {

    public class MyModClass {

        #region Mod Information
        //Name set in Project>ProjectName Properties>Application>Assembly Name
        public string Creator = "Hollings" , Version = "V1.0.0";
        public int enemiesKilled = 0;
        public string Description = "Enemies can only be killed by dismembering a specific arm, leg, or head. Weak spots are indicated by armor on that body part.";
        public bool audienceEnlarged = false;
        public string ModName { get {return Assembly.GetExecutingAssembly().GetName().Name; } } //IGNORE THIS (Thanks Taco)
        #endregion

        public void Init ()
        {
            //This is called when the game starts.



            Debug.Log ( ModName + " has finished initializing!" );
        }

        #region Additional Methods
      
        public void OnUpdate ()
        {
            // This is just some silly stuff to turn the audience into giant eyeball people.
            if (!audienceEnlarged)
            {
                NaziTalkingHead[] heads = UnityEngine.Object.FindObjectsOfType<NaziTalkingHead>();


                // Need to wait for the audience to actually spawn before editing them.
                if (heads.Length > 1)
                {
                    foreach (NaziTalkingHead head in heads)
                    {

                        // Make one eye giant
                        head.leftEye.localScale += new Vector3(8F, 8F, 8F);

                        // And put it right in the middle of his face.
                        head.leftEye.localPosition = Vector3.Lerp(head.leftEye.localPosition, head.rightEye.localPosition, 0.5f);

                    }
                    audienceEnlarged = true;
                }
                
            }
            

        }

        public void OnFixedUpdate ()
        {
           
        }

        public void OnLevelLoaded (Scene scene, LoadSceneMode mode)
        {
           
            //called when a level is loaded
            
        }

        public void OnGUI ()
        {
            //called several times per frame (one call per event) - Used for GUI elements
        }
        #endregion
        
        #region Gorn callbacks
        public void OnEnemyDie (Enemy enemy, Crab crab, Bird bird)
        {
            //Called when an enemy dies.
            // I'm thinking of using total deaths as a sort of difficulty 
            enemiesKilled += 1;

            if (enemy != null) {
                //Regular enemy died
            } else if (crab != null) {
                //Crab died
            } else if (bird != null) {
                
            }
        }

        public void OnEnemySetUp (EnemySetupInfo esi)
        {

            if (enemiesKilled >= 1)
            {
             
                //Called before OnEnemySpawn
                //esi.speedBonus = enemiesKilled;
                switch (UnityEngine.Random.Range(0,3))
                {
                    case 0:
                        esi.armorType = ArmorType.Heavy;
                        break;
                    case 1:
                        esi.armorType = ArmorType.Leather;
                        break;
                    case 2:
                        esi.armorType = ArmorType.Steel;
                        break;
                }
                esi.belt = false;
                esi.leftFootArmor = false;
                esi.leftKneepad = false;
                esi.leftHandArmor = false;
                esi.leftPauldron = false;
                esi.leftShinArmor = false;
                esi.leftThighArmor = false;
                esi.leftLowerBracer = false;
                esi.leftUpperBracer = false;
                esi.rightFootArmor = false;
                esi.rightKneepad = false;
                esi.rightHandArmor = false;
                esi.rightPauldron = false;
                esi.rightShinArmor = false;
                esi.rightThighArmor = false;
                esi.rightLowerBracer = false;
                esi.rightUpperBracer = false;
                esi.breastPlate = false;
                esi.helmet = false;

                switch (enemiesKilled % 5)
                {
                    case 0:
                        esi.helmet = true;
                        esi.breastPlate = true;
                        break;

                    case 1:
                        esi.rightLowerBracer = true;
                        esi.rightHandArmor = true;
                        break;

                    case 2:
                        esi.leftShinArmor = true;
                        esi.leftFootArmor = true;
                        break;
                    case 3:
                        esi.rightShinArmor = true;
                        esi.rightFootArmor = true;
                        break;
                    case 4:
                        esi.leftLowerBracer = true;
                        esi.leftHandArmor = true;
                        break;
                    default:
                        break;
                }
            }
        }

        public void OnEnemySpawn (Enemy enemy, Crab crab)
        {
            if (enemiesKilled >= 1)
            {

                //enemy.speedMod = enemiesKilled;
                enemy.maxHealth = 666;
                enemy.health = 666;
                enemy.scale = UnityEngine.Random.Range(0.5F, 1.50F);
                

                //Enemy shoulderGuy = new Enemy();
                //shoulderGuy.type = SpecialEnemyType.ShoulderLackey;

                //ShoulderSeatArmorPiece shoulderSeat = new ShoulderSeatArmorPiece();
                //shoulderSeat.armoredTo = enemy.rightUpperArm;

                //enemy.MountShoulder(shoulderGuy, shoulderSeat);
                enemy.head.killOwnerOnSever = false;
                enemy.chest.killOwnerOnSever = false;
                enemy.lowerChest.killOwnerOnSever = false;
               
                //enemy.head.leftEye.Enlarge(2);
                //enemy.head.rightEye.Enlarge(2);

                switch (enemiesKilled % 5)
                {
                    case 0:
                        enemy.head.killOwnerOnSever = true;
                        break;
                    case 1:
                        enemy.rightUpperArm.killOwnerOnSever = true;
                        break;
                    case 2:
                        enemy.leftThigh.killOwnerOnSever = true;
                        break;
                    case 3:
                        enemy.rightThigh.killOwnerOnSever = true;
                        break;
                    case 4:
                        enemy.leftUpperArm.killOwnerOnSever = true;
                        break;
                    default:
                        break;
                }
            }

            if (enemy != null) {
                //Regular Gornie spawned
            } else if (crab != null) {
                //Crab Spawned
            }
        }

        public void OnPlayerDied ()
        {
            //Called when player dies
        }

        #region User Requests

        public void OnRegisterCrab ()
        {
            //Called when Crab registers in scene - [USER REQUEST]
        }

        public void OnBowFire (Bow bow)
        {
            //Called when a bow is fired
        }

        #endregion

        #endregion
    }
}