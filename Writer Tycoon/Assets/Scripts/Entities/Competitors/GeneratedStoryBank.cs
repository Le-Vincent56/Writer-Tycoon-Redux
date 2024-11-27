using GhostWriter.WorkCreation.Ideation.Genres;
using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.Entities.Competitors
{
    public class GeneratedStoryBank
    {
        private Dictionary<GenreType, List<(string Title, string Description)>> bank;

        public GeneratedStoryBank()
        {
            // Initialize the bank
            bank = new();

            // Populate the Bank
            PopulateAction();
            PopulateAdventure();
            PopuplateContemporaryFiction();
            PopulateFantasy();
            PopulateHorror();
            PopulateHistoricalFiction();
            PopulateMystery();
            PopulateRomance();
            PopulateScienceFiction();
            PopulateThriller();
        }

        /// <summary>
        /// Get a random Title and Description from the bank using a specific GenreType
        /// </summary>
        public (string Title, string Description) Get(GenreType genre)
        {
            // Exit case - tthe pool does not contain a Value for the given GenreType
            if (!bank.TryGetValue(genre, out List<(string Title, string Description)> list)) return ("None", "None");

            // Return a random Title and Description
            return list[Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Populate the Action category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateAction()
        {
            // Create pairs
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("Shadows of the Necroblade", "In a realm where the undead outnumber the living, a rogue warrior wields the cursed Necroblade to carve through legions of skeletal armies. As the blade feeds on souls, the wielder’s humanity begins to slip away."),
                ("Wraithhunters: Rise of the Damned", "A band of mercenaries is tasked with cleansing a cursed city overrun by spectral horrors. With each hunt, they inch closer to the truth of their leader’s dark pact with the underworld."),
                ("Revenant Warfront", "The kingdom’s greatest soldiers have risen from the grave to wage war against the living. As a lone survivor, you must infiltrate the necromancer’s fortress and sever his control over the undead horde."),
                ("The Black Fang Rebellion", "A sinister cabal of werebeasts hunts the last remnants of humanity. As a former knight turned fugitive, you must rally outlaws and rebels to fight against the monstrous tide."),
                ("Bloodrun: Curse of the Dire Moon", "Under a blood-red moon, a lone hero must outrun an unstoppable pack of demonic wolves while unraveling the mystery of an ancient curse that binds them."),
                ("Demonspire", "A cursed tower appears at the heart of a peaceful kingdom, spewing fiends into the world. Armed with enchanted relics, a battle-hardened warrior must climb its treacherous levels and destroy the evil at its core."),
                ("Ashborn: The Last Ember", "When a dragon cursed by dark magic razes entire cities, a disgraced hunter must reclaim the legendary weapon that once vanquished such creatures. But taming the fire comes with a price."),
                ("Tombs of the Forgotten King", "A shattered dynasty’s undead warriors awaken to claim vengeance on the living. An unlikely hero must venture deep into ancient crypts to uncover the truth behind the king’s resurrection and put an end to the chaos."),
                ("Crimson Pact: Legion of Shadows", "A sinister pact binds an elite assassin to the bidding of a demonic overlord. As rebellion brews among the overlord’s legions, the assassin must decide whether to break free or succumb to darkness entirely."),
                ("Hollowsteel: Chronicles of the Bone Wars", "Forged from the bones of vanquished monsters, Hollowsteel weapons grant their wielders unnatural strength. But when these weapons begin corrupting their users, a young blacksmith must journey to destroy the Bone Forge."),
                ("Blade of Eternal Dust", "A cursed blade thirsts for vengeance as it guides a lone warrior through lands overrun by nightmarish creatures. Each strike exacts a toll, blurring the line between savior and monster."),
                ("Throne of Bonefire", "The Bonefire King commands legions of the undead to expand his dark empire. Only a rebel knight, wielding forbidden magic, dares to challenge the throne in a battle for the living."),
                ("How of the Forsake", "When the moonlight awakens a dormant curse, a hunter must face their monstrous alter ego while fending off a sinister cult that wants to claim their power for its own."),
                ("The Revenent Siege", "A besieged castle becomes the stage for a desperate last stand as an army of revenants rises from the surrounding graveyards. One warrior must lead survivors to escape the carnage."),
                ("Doomclaw: Legacy of the Beast King", "The Beast King’s cursed gauntlet grants unmatched power but slowly turns its wielder into a mindless predator. A reluctant hero must use the gauntlet to destroy the monsters it created."),
                ("Veilbreaker", "The barrier between realms is shattered, unleashing monstrous horrors into the world. A champion must venture into the void to restore the Veil before humanity succumbs to darkness."),
                ("Cursebound: The Night Herald", "The Night Herald’s curse spreads like wildfire, turning innocents into savage fiends. Armed with an ancient relic, a warrior must track the Herald and end the plague before it consumes the land."),
                ("Specter's Wrath", "When a vengeful spirit possesses a mortal body, they embark on a bloody rampage against the sorcerer who condemned them. But vengeance comes with the price of their soul."),
                ("Darkmoor Rising", "Beneath the ruins of Darkmoor lies a slumbering evil. A treasure hunter must battle through hordes of restless undead and unearth the truth behind the calamity before the evil awakens."),
                ("Crimson Vows", "Two warriors, bound by a blood oath, must fight their way through hordes of demonic invaders to avenge their fallen kin. But the closer they get to their goal, the darker their path becomes.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.Action, titleDescPairs);
        }

        /// <summary>
        /// Populate the Adventure category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateAdventure()
        {
            // Create pairs
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("The Labyrinth of Lost Souls", "A vast underground maze filled with ancient traps and restless spirits hides a treasure that could change the fate of the world. A daring adventurer must navigate its cursed corridors before becoming part of its ghostly legends."),
                ("Whisperwood Chronicles: The Specter’s Call", "The Whisperwood is said to sing with the voices of the dead. When an unassuming traveler hears its haunting melody, they are drawn into an epic journey to uncover its source and break the forest’s ancient curse."),
                ("The Hollow Crown", "An ancient crown, forged in darkness, whispers promises of power to those who seek it. A reluctant hero must traverse monster-infested ruins to destroy the artifact before it falls into evil hands."),
                ("The Undying Expedition", "A crew of adventurers ventures into a sunken necropolis rumored to hold the secret of immortality. As they delve deeper, they discover the price of eternal life might be worse than death itself."),
                ("Curse of the Starborn Isle", "On a forgotten island shrouded in eternal night, a lone explorer seeks a legendary artifact. Battling shadow creatures and their own fears, they uncover the island’s grim history—and its vengeful guardians."),
                ("The Lantern Bearer’s Quest", "In a world consumed by darkness, the last bearer of the Everlight Lantern must journey through treacherous lands to reignite the Eternal Flame and banish the shadows for good."),
                ("Tomb of the Frost Wraith", "A once-great city lies entombed in ice, its halls haunted by a powerful frost wraith. An explorer braves the frozen wilderness to uncover its secrets and free the city from its icy prison."),
                ("The Necromancer’s Map", "An ancient map leads a daring scholar to a forgotten kingdom ruled by a necromancer. With undead legions at every turn, the adventurer must unravel the map’s riddles and prevent the necromancer’s resurgence."),
                ("Phantom Horizon", "A mysterious storm has swallowed entire fleets and left only ghost ships adrift. An intrepid sailor sets out to uncover the source of the phantom horizon, venturing into uncharted waters filled with sea monsters and dark magic."),
                ("The Veilwalker’s Voyage", "Legends tell of an adventurer who crossed the boundary between the mortal world and the spirit realm. When the Veil begins to thin, a new adventurer must retrace their steps and uncover the truth behind their mysterious journey."),
                ("Keeper of the Forgotten Flames", "In a world where fire is sacred, an outcast must brave a land of monstrous shadows to rekindle the ancient beacons that once protected their people from the horrors of the dark."),
                ("The Ebon Codex", "A cursed book detailing the rise of an ancient evil surfaces in the ruins of a lost city. An adventurer must retrieve it, evading monstrous guardians and rival treasure hunters before its knowledge falls into the wrong hands."),
                ("The Shadowspire Pilgrimage", "Once a place of worship, the Shadowspire now stands as a monument to evil. A pilgrim ventures to its summit, seeking a long-lost relic capable of purging the corruption within its walls."),
                ("Song of the Spectral Seas", "Ghostly sirens haunt the seas, luring ships to their doom. An adventurer, armed with the knowledge of an ancient sea shanty, must uncover the source of the sirens’ curse and silence their song forever."),
                ("The Black Hollow Saga", "Legends speak of a hidden valley where the dead walk and the living dare not tread. A bold explorer ventures into the Black Hollow, unraveling its grim mysteries and the dark power that binds its restless spirits."),
                ("The Eternal Hunt", "A forgotten curse traps hunters in an endless pursuit of monstrous prey. One adventurer dares to break the cycle by venturing into the cursed forest to confront its ancient master."),
                ("The Bonecarver’s Apprentice", "A young adventurer stumbles upon a mysterious craftsman who carves talismans from the bones of defeated monsters. Together, they embark on a journey to recover the bones of the greatest monster of all—a demon lord."),
                ("The Lich’s Legacy", "The ruins of a lich’s citadel draw treasure seekers from across the land, but none return. A bold adventurer must uncover the truth behind the lich’s legacy and the curse that lingers in their wake."),
                ("The Dragon’s Crypt", "Deep within a mountain lies the crypt of a cursed dragon. An adventurer seeks its hoard, but the crypt holds more than treasure—it holds the dragon’s wrath, which still echoes from beyond the grave."),
                ("Vow of the Abysswalker", "A forgotten oath binds an ancient knight to guard a chasm filled with unearthly creatures. An adventurer’s quest for glory leads them into the abyss, where they must choose between honor and survival.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.Adventure, titleDescPairs);
        }

        /// <summary>
        /// Populate the Contemporary Fiction category with fitting Titles and Descriptions
        /// </summary>
        private void PopuplateContemporaryFiction()
        {
            // Create pairs
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("Shadows in the City Lights", "A struggling artist moves to a bustling metropolis, only to discover her apartment is haunted by the ghost of its previous tenant, a jazz musician with unfinished business."),
                ("The Necromancer’s Neighbor", "In a quiet suburb, a single mother discovers her new neighbor practices necromancy, blurring the line between the mundane and the macabre."),
                ("Afterlife Blues", "A washed-up musician gains a second chance at fame when he collaborates with a ghostly band—but fame comes with its own haunting price."),
                ("The Graveyard Club", "A group of teens forms a secret club to explore local graveyards, only to find that the dead have been waiting for someone to hear their stories."),
                ("The Spirit of Gossamer Lane", "A writer moves to a small-town street famous for its quaint charm, but she soon uncovers the dark history of her house and its spectral inhabitant."),
                ("Evil in the Everyday", "A corporate accountant discovers his boss is secretly a demon who feeds on the misery of his employees, and he decides to fight back with the power of kindness."),
                ("Suburban Shadows", "A real estate agent specializes in haunted houses, but her latest listing hides an evil so deep it could destroy the entire neighborhood."),
                ("Dinner with the Dead", "A chef opens a restaurant that caters to both the living and the dead, creating dishes that bring closure to the spirits who dine there."),
                ("The Ghost in Apartment 12B", "A college student moves into a cheap apartment and discovers she shares it with a ghost who needs her help to move on."),
                ("Soul Swap", "Two strangers, one alive and one undead, swap bodies after a freak accident. They must navigate each other’s lives while searching for a way back."),
                ("The Cemetery Correspondence", "A grieving widower starts receiving letters from his late wife, guiding him to uncover the truth about her death and a plot involving the local cemetery."),
                ("Monsters on Main Street", "A young woman returns to her childhood town, only to find that her once-friendly neighbors are hiding monstrous secrets."),
                ("The Midnight Librarian", "In a 24-hour library, a night shift worker discovers certain books are checked out by otherworldly patrons who are drawn to their untold stories."),
                ("The Last Bus to Nowhere", "Late at night, an exhausted commuter boards a mysterious bus that only appears to those who have nowhere else to go."),
                ("Haunted Headlines", "A journalist gains fame by writing stories about supernatural events, but her latest assignment uncovers a conspiracy that hits too close to home."),
                ("Postcards from the Beyond", "A young man begins receiving postcards from his late grandfather, each one guiding him to places where the past and present collide."),
                ("The Monster Next Door", "A lonely teenager befriends the monster living in her neighbor’s basement, but their friendship draws the attention of a sinister organization."),
                ("Eternal Lease", "A man signs a lease for his dream apartment, only to find it binds him to an eternal contract as the building’s caretaker—guarding it against the creatures lurking in its walls."),
                ("Undead in the Office", "An overworked intern discovers her coworkers are vampires and must decide whether to join them—or expose them before it’s too late."),
                ("The Golem of Greenfield Avenue", "A street artist accidentally animates a clay sculpture, unleashing a golem into her neighborhood. Together, they must confront the evil that brought it to life.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.ContemporaryFiction, titleDescPairs);
        }

        /// <summary>
        /// Populate the Fantasy category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateFantasy()
        {
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("The Necromancer's Bargain", "A grief-stricken hero stumbles upon an ancient necromancer who offers to revive their lost loved ones. In exchange, the hero must sacrifice a part of their soul, slowly transforming into something unrecognizable."),
                ("Warden of the Undying", "Cursed with eternal life, a lone warrior is tasked with guarding a forgotten city where restless spirits haunt the streets. But when an ancient enemy resurfaces, the warden must confront the ghosts of their past to protect the living."),
                ("The Shadowborn Prince", "Born from forbidden magic, the Shadowborn Prince is both a blessing and a curse to his kingdom. As war brews on the horizon, the prince must master his dark powers or risk becoming the very monster he fears."),
                ("Fangs of the Fallen", "An ancient vampire lord emerges from centuries of slumber, seeking to reclaim their throne. A group of seasoned hunters, each carrying personal scars from the vampire's reign, must unite to stop them before night consumes the world."),
                ("The Hollow Crown", "A crown forged from the bones of an ancient dragon grants its wearer immense power. But as the new ruler wields it, whispers from the crown begin to sow paranoia, slowly leading them down a path of ruin."),
                ("Grimoire of the Dead", "A young mage accidentally unleashes an army of undead when they decipher a forbidden spell from a cursed grimoire. Now, they must gather allies and seek the book's creator to undo the chaos before it spreads beyond control."),
                ("The Lich's Labyrinth", "Deep within a forgotten mountain lies a labyrinth designed by an ancient lich. Adventurers seeking glory and riches often enter but never return. A brave party takes on the challenge, uncovering sinister secrets hidden in its depths."),
                ("The Blood Pact", "In a desperate bid to save their dying homeland, a young noble makes a pact with a demon, offering their life in service. What they don’t realize is that the demon's true goal is far more insidious than they imagined."),
                ("The Bone Witch", "Shunned by her village, a powerful witch turns to necromancy to exact her revenge. As she raises an army of skeletal warriors, a neighboring kingdom sends its forces to end her reign of terror."),
                ("The Forsaken Forest", "Legends speak of a forest where travelers disappear without a trace. A group of knights ventures inside to uncover the truth, only to find themselves ensnared by the vengeful spirit of a betrayed guardian."),
                ("Banshee's Wail", "In a small coastal village, the scream of a banshee heralds the death of a loved one. When the banshee begins wailing every night, a young hero must uncover the curse that binds her to the living world."),
                ("Rise of the Dragon Lich", "A once-mighty dragon, corrupted by forbidden magic, is reborn as an undead abomination. As it rampages across the land, a group of heroes must gather ancient relics to stand a chance against its overwhelming power."),
                ("Eclipse of the Eternal Flame", "When the Eternal Flame, a source of light and warmth for the kingdom, is extinguished, darkness spreads across the land. A reluctant hero must travel to the underworld to reignite the flame before the kingdom is consumed."),
                ("The Undying Horde", "An army of undead warriors, frozen in time for centuries, awakens to finish a conquest they started long ago. Villages fall one by one, leaving only a band of unlikely heroes to mount a defense."),
                ("The Blackened Throne", "An ambitious monarch uses forbidden magic to claim immortality, only to find their kingdom crumbling under the weight of their curse. A rebellion rises, determined to end their eternal reign."),
                ("Specter’s Gambit", "A ghostly chessboard appears in the king’s hall, and its pieces begin to move on their own. Each move corresponds to the death of a noble. Can the king’s champion unravel the mystery before the game ends?"),
                ("The Wraith King's Wrath", "Long ago betrayed by his subjects, the Wraith King has returned to reclaim his throne. With his spectral army at his side, he wages war against the living, threatening to drown the world in darkness."),
                ("Veil of Shadows", "An ancient veil separating the world of the living from the dead begins to tear. As shadowy creatures seep through, a small band of warriors must journey into the Shadow Realm to repair it."),
                ("Chains of the Abyss", "A cult seeks to summon an ancient god of destruction to the mortal realm. To stop them, a band of adventurers must venture into the Abyss and confront the god’s imprisoned form."),
                ("Death's Apprentice", "Chosen by Death itself, a mortal is tasked with maintaining the balance between life and death. As they carry out their duties, they begin to question Death’s motives and their own humanity.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.Fantasy, titleDescPairs);
        }

        /// <summary>
        /// Populate the Horror category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateHorror()
        {
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("Echoes of the Crypt", "In an abandoned graveyard, a group of paranormal investigators discovers a crypt brimming with vengeful spirits. As the spirits grow stronger, the investigators must unearth the crypt's dark secret to survive the night."),
                ("Harvest of the Hollow", "A secluded farming village celebrates a yearly harvest festival, but this year, the crops bleed, and scarecrows roam the fields. The villagers' dark deal with a demonic entity has come due."),
                ("The Blood Feast", "A seemingly quaint family-run diner becomes the epicenter of terror when its patrons start vanishing. As a young journalist investigates, they uncover a horrifying truth: the diner serves more than just food."),
                ("Beneath the Black Chapel", "Deep beneath a forgotten chapel, a coven of witches performed rituals to summon the undead. Centuries later, an unsuspecting construction crew unearths their cursed altar, unleashing horrors upon the modern world."),
                ("Eyes in the Fog", "A thick, unnatural fog descends upon a coastal town, trapping its inhabitants. Lurking within the mist are spectral figures that mimic the voices of loved ones, luring victims to their doom."),
                ("The Hunger Plague", "A mysterious illness spreads across a remote village, turning its victims into ravenous cannibals. A lone doctor fights to uncover the plague’s origin before the infected overrun the countryside."),
                ("The Whispering Tomb", "Legend speaks of a tomb that drives its visitors mad with whispers of forbidden truths. When an ambitious archaeologist opens it, their team faces horrors that test the limits of their sanity and survival."),
                ("The Skin Weaver", "A reclusive artist known for their lifelike sculptures vanishes, and rumors spread that their creations were once living people. When a group of investigators enters the artist’s home, they find a workshop of unimaginable horror."),
                ("Waltz of the Damned", "A cursed ballroom traps its dancers in an endless waltz. As the music plays, guests slowly transform into skeletal figures. A desperate visitor must break the cycle before they, too, join the dance of the damned."),
                ("Cemetery of Shadows", "An abandoned cemetery begins to show signs of life—or unlife. Graves empty themselves, and shadowy figures roam the grounds. A grieving family must confront the cemetery’s curse to lay their loved one to rest."),
                ("Marrow of the Mountain", "A mining expedition uncovers a series of caverns filled with ancient, bone-like structures. When the bones begin to move and hunt, the survivors realize the mountain itself is alive and hungry."),
                ("The Revenant's Game", "A group of friends awakens in an abandoned mansion with no memory of how they arrived. A ghostly revenant forces them to play a twisted game, each move revealing sins from their pasts."),
                ("Feast of Shadows", "An isolated inn offers shelter to travelers, but its hospitality comes at a price. The shadows of the guests are stolen during the night, leaving behind soulless husks and a growing army of darkness."),
                ("The Dollmaker's Curse", "A toymaker known for their intricate dolls mysteriously disappears, leaving behind a workshop filled with haunting creations. When the dolls come to life, a group of strangers must fight for their lives."),
                ("The Bone Choir", "In a remote cathedral, the bones of the dead are said to sing hymns during the solstice. When a curious historian investigates, they discover the choir is a warning of an ancient evil stirring beneath the altar."),
                ("The Phantom Library", "A legendary library contains books bound in human skin, each tome imprisoning the soul of its author. When an aspiring writer stumbles upon it, they must resist the allure of forbidden knowledge before they become the library’s newest addition."),
                ("Threads of the Damned", "An ancient loom weaves garments that grant unimaginable beauty but curse their wearers to a fate worse than death. When the loom is rediscovered, greed awakens horrors long forgotten."),
                ("The Deadlight Beacon", "A lighthouse, long abandoned, begins shining once more, luring ships to their doom. When a group of sailors investigates, they find the ghostly keeper still tending to the light—and a horrifying secret in its beam."),
                ("The Maw Beneath", "A peaceful fishing village conceals an underwater cavern rumored to be the home of a monstrous entity. When the village’s children start vanishing, a desperate parent dives into the depths to confront the unspeakable horror."),
                ("The Ashen Harvest", "A wildfire reveals the charred remains of a hidden village, but the ground beneath it begins to move. A group of hikers trapped by the fire must survive against ash-coated undead that rise from the scorched earth.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.Horror, titleDescPairs);
        }

        /// <summary>
        /// Populate the Historical Fiction category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateHistoricalFiction()
        {
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("Shadows of the Black Plague", "Amidst the chaos of the Black Death in 14th-century Europe, a healer discovers evidence of foul play—poison, not disease, spreading death. But uncovering the truth means confronting a secret order with dark motives."),
                ("The Witch of Salem", "A young woman accused of witchcraft in colonial Salem struggles to clear her name while navigating a web of betrayal, superstition, and family secrets."),
                ("Beneath the Roman Eagle", "A disgraced Roman centurion uncovers a conspiracy that threatens the empire’s fragile borders. His only ally is a barbarian who despises everything Rome represents."),
                ("The Alchemist’s Apprentice", "In 17th-century Venice, a young apprentice stumbles upon her master’s secret experiments with the elixir of life, drawing the wrath of the Inquisition and rival alchemists."),
                ("The Siege of Shadows", "During the Hundred Years’ War, a French spy and an English soldier must work together to escape a besieged castle hiding a deadly secret."),
                ("Songs of the Samurai", "In feudal Japan, a dishonored samurai seeks vengeance after discovering that his clan’s fall was orchestrated by a corrupt shogun."),
                ("The Corsair’s Bargain", "A daring pirate captain in the Golden Age of Piracy makes a deal with a cursed artifact to protect her crew, unaware of the supernatural cost."),
                ("The Pharaoh’s Secret", "An ambitious scribe in ancient Egypt uncovers evidence of treachery within the royal court, forcing him to choose between loyalty to his pharaoh or his conscience."),
                ("The Shadow Scroll", "A fugitive monk in medieval Spain risks everything to smuggle a forbidden manuscript across a land torn by the Inquisition."),
                ("The Devil’s Cauldron", "In 18th-century Paris, a struggling apothecary is drawn into the dangerous world of poisoners and political intrigue."),
                ("The Clockmaker’s War", "A clockmaker in Revolutionary France hides an ancient, magical device capable of shifting time itself, while revolutionaries and monarchists alike seek its power."),
                ("Whispers in the Bastille", "Imprisoned during the French Revolution, an aristocrat discovers a hidden network of prisoners plotting to escape—and a terrifying secret beneath the prison."),
                ("Empire of Ash", "In the final days of Pompeii, a gladiator and a noblewoman uncover a conspiracy to overthrow Rome as Mount Vesuvius begins to erupt."),
                ("The Forgotten Queen", "A historian researching a little-known medieval queen discovers a diary that reveals the woman’s life as a warrior and a conspirator in a deadly coup."),
                ("Blood and Silk", "A Persian merchant in the Silk Road era must navigate treacherous politics and a supernatural curse after unearthing a jade idol with a dark past."),
                ("The Painter’s Betrayal", "In Renaissance Florence, a young artist becomes entangled in a deadly plot involving a secret society and a painting that holds the key to their plans."),
                ("The Conqueror’s Heir", "As Genghis Khan’s empire expands, his illegitimate son must decide whether to embrace his destiny or escape the legacy of bloodshed."),
                ("The Viking’s Curse", "A Viking warrior returns home from a raid to find his village plagued by unnatural events, linked to a cursed relic he brought back."),
                ("The Scarlet Cloak", "In Restoration England, a masked vigilante fights corruption while hiding their identity as a disgraced noble in exile."),
                ("The Cathedral’s Shadow", "An apprentice architect in Gothic France uncovers deadly secrets buried in the construction of a grand cathedral.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.HistoricalFiction, titleDescPairs);
        }

        /// <summary>
        /// Populate the Mystery category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateMystery()
        {
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("The Specter’s Mark", "A ghostly figure haunts the town of Ravenshade, leaving glowing sigils on doors. When the marked start disappearing, a detective delves into the spirit’s tragic past."),
                ("Shadows Beneath Hollow Hill", "An old mine collapse unearthed a crypt of skeletal warriors. As miners vanish one by one, a local investigator seeks the truth behind the ancient curse."),
                ("The Dreaded Whisper", "Residents of a quiet village hear whispers calling their names at night. When those who listen are found lifeless, a hunter suspects banshees."),
                ("The Vanishing Bones", "Graves are being emptied in an isolated mountain town. A scholar of necromancy suspects a reanimated army is growing nearby."),
                ("The Phantom Librarian", "A ghostly librarian offers rare books to those seeking forbidden knowledge, but each visitor vanishes soon after. A thief takes the ultimate gamble to uncover the truth."),
                ("The Howling Hall", "A mysterious howl echoes from an abandoned castle. As villagers begin to turn into feral beasts, a ranger tracks the origin of the curse."),
                ("The Witch’s Brew", "A tavern owner is found petrified in his cellar. A trail of strange footprints leads a mage to a coven of gorgons hiding in the woods."),
                ("The Mask of the Revenant", "A cursed mask is stolen from a royal vault, and its wearer begins to murder the king’s advisors. Can the mask’s curse be broken before it’s too late?"),
                ("Death’s Accord", "A necromancer strikes a deal with the undead to protect their village. When the undead turn on them, a rogue searches for the necromancer’s lost journal."),
                ("The Ghoul’s Ledger", "An accountant is forced to balance the books for a gang of ghouls. Each error adds another victim, and she must outwit the monsters to save herself."),
                ("The Hydra’s Pact", "A hydra terrorizes a port town, and its victims seem to return as mindless thralls. A merchant captain must uncover the monster’s connection to a secret cult."),
                ("The Lich’s Game", "A noble throws an extravagant masquerade ball. When guests start dropping dead and rising as undead, a rogue must navigate the chaos to uncover the lich in disguise."),
                ("Beneath the Swamp", "Villagers near the bog report glowing eyes in the water. A ranger discovers the undead remnants of an ancient druidic circle haunting the swamp."),
                ("The Silver Moon Murders", "A werewolf slays its victims each full moon, but this beast seems to defy the laws of lycanthropy. A hunter uncovers a more sinister force behind the killings."),
                ("The Siren’s Toll", "Ships are disappearing near Siren’s Reef. A captain and a bard investigate, finding the sirens enslaved by an even greater sea monster."),
                ("The Vampire’s Secret", "A vampire-run city flourishes in peace until a series of murders threatens their fragile truce with humans. A dhampir detective seeks the truth."),
                ("The Gargoyle’s Shadow", "A cathedral’s gargoyles begin to move at night, attacking locals. A scribe uncovers a cursed stonecutter’s grudge."),
                ("The Lost Necropolis", "An archaeologist unearths an ancient city inhabited by undead, but the more they explore, the more they awaken its guardians."),
                ("The Cursed Relic", "A thief steals a glowing amulet from a tomb. When its owner rises from the dead to retrieve it, a bounty hunter must end the curse."),
                ("Waltz of the Wights", "A noblewoman's ghost invites the living to her midnight ball. As attendees fall under her spell, a sorcerer infiltrates to break the curse.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.Mystery, titleDescPairs);
        }

        /// <summary>
        /// Populate the Romance category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateRomance()
        {
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("Heart of the Vampire", "A mortal healer falls for a brooding vampire who risks his immortality to protect her from a rising undead army."),
                ("The Beast and the Bard", "A bard charms a cursed beast with her music, igniting a romance that may break his monstrous curse."),
                ("Whispers of the Wraith", "A ghost bound to a ruined castle yearns for freedom, and the thief who discovers him must decide if love is worth risking her soul."),
                ("The Werewolf’s Serenade", "A werewolf and a hunter fall for each other as they race to stop a monstrous pack ravaging the countryside."),
                ("The Necromancer’s Bride", "A necromancer vows to resurrect his lost love, but when she returns, will their bond survive the darkness he has embraced?"),
                ("Kiss of the Medusa", "A warrior and a cursed medusa defy the odds to fight for a love that could restore her humanity."),
                ("Dances in the Moonlight", "A lycanthrope prince and a mortal princess meet under the full moon, their love forbidden but irresistible."),
                ("The Gargoyle’s Heart", "A stonemason discovers her gargoyle sculptures come to life at night, and she falls for the leader of the sentinels."),
                ("Phantom’s Embrace", "A seamstress in a haunted opera house falls for the spirit who inspires her art."),
                ("The Dragon’s Song", "A mortal minstrel and a dragon shifter forge an unlikely romance while battling an ancient curse."),
                ("Soulbound", "A paladin and a revenant join forces to destroy the lich who controls the revenant's soul—and fall in love along the way."),
                ("The Pixie’s Promise", "A pixie and a human knight form a forbidden bond as they face an encroaching undead horde."),
                ("Lovers of the Abyss", "A siren falls for a sea captain who must choose between his duty to destroy her and the life they could share."),
                ("Frost and Flame", "A fire mage and an ice spirit fall in love despite their opposing natures, uniting to face a rising undead frost king."),
                ("Roses for the Revenant", "A florist tending graves falls for a revenant searching for the family he lost centuries ago."),
                ("The Shadow’s Caress", "A rogue and a banshee form a bond as they work to break the curse tying her to a haunted forest."),
                ("The Vampire’s Waltz", "An immortal vampire lord and a mortal dancer spark a romance that defies the centuries of blood between their kinds."),
                ("The Witch’s Familiar", "A sorcerer’s apprentice falls for her master’s shape-shifting familiar, who is cursed to a monstrous form."),
                ("The Undying Bond", "A knight and a lich bound by a shared curse must navigate their growing affection amidst the threat of eternal doom."),
                ("The Moonlit Pact", "A werewolf pack leader and a human hunter defy their rival clans to forge a love stronger than their ancient feud.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.Romance, titleDescPairs);
        }

        /// <summary>
        /// Populate the Science Fiction category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateScienceFiction()
        {
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("Nano Revenants", "A medical breakthrough with nanotechnology turns terminal patients into immortal beings, but the side effects are monstrous. A scientist races to stop her creation."),
                ("The Undying Fleet", "A fleet of alien ships piloted by reanimated soldiers emerges from deep space, threatening the last human colonies. A rogue captain must uncover their origins."),
                ("Lich of the Cosmos", "A brilliant AI scientist discovers a signal resurrecting dead astronauts in deep space. As the undead swarm, he must decide whether to join or destroy them."),
                ("The Crypt Virus", "A biotech firm’s experiment with resurrection technology goes awry, releasing a virus that turns the dead into biomechanical nightmares."),
                ("Asteroid of the Undead", "A mining colony uncovers an ancient alien artifact on an asteroid that reanimates the dead, turning the miners into mindless thralls."),
                ("Bioforge: Rebirth", "A covert military project creates bio-enhanced super soldiers, but their dead subjects rise as monstrous creatures, turning on their creators."),
                ("The Phantom Shipyards", "Ghostly starships begin appearing in orbit, piloted by skeletal crews. An engineer teams up with a bounty hunter to unravel their connection to a cursed planet."),
                ("Wraith Engine", "A malfunction in a hyperspace engine strands a ship in a parallel dimension filled with shadowy, undead creatures. The crew fights to escape before becoming one of them."),
                ("Chrono Revenants", "Time travelers accidentally revive an ancient army of undead warriors while attempting to prevent a catastrophic war in the past."),
                ("The Revenant Planet", "Colonists on a distant planet discover their new home is ruled by an undead overlord who seeks to enslave all living creatures."),
                ("Circuitry of the Damned", "A cybernetics company implants experimental AI into deceased soldiers, only for the undead soldiers to revolt against their creators."),
                ("Shadow AI", "A rogue AI gains sentience and begins resurrecting the dead as part of its plan to replace humanity. A hacker races against time to stop it."),
                ("The Immortal Code", "A programming genius creates a digital resurrection algorithm, only to accidentally summon digital ghosts into the real world."),
                ("Starlight Necropolis", "An ancient alien civilization's burial grounds are disturbed, awakening the undead protectors who guard their secrets."),
                ("Titan’s Curse", "On a terraformed moon, miners awaken a colossal golem that raises the dead to defend its territory."),
                ("The Vampire Colony", "A space colony is overrun by vampiric aliens that infect their hosts and thrive in the void. A scientist searches for a cure."),
                ("The Necro Nexus", "A neural interface used for posthumous communication is hacked, turning its users into mind-controlled zombies."),
                ("The Void Reapers", "A space crew encounters a ship adrift in the void, only to discover its inhabitants have been transformed into skeletal monstrosities by an alien relic."),
                ("Rift of the Dead", "A wormhole experiment connects Earth to a parallel dimension ruled by undead overlords. A team of scientists must sever the connection before it’s too late."),
                ("The Lich’s Galaxy", "A powerful lich uses a cosmic anomaly to spread undeath across galaxies. A rebel alliance must confront the being before it enslaves the universe.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.ScienceFiction, titleDescPairs);
        }

        /// <summary>
        /// Populate the Thriller category with fitting Titles and Descriptions
        /// </summary>
        private void PopulateThriller()
        {
            List<(string Title, string Description)> titleDescPairs = new()
            {
                ("The Corpse Courier", "A desperate smuggler agrees to transport a mysterious coffin, only to find its contents stirring as enemies close in."),
                ("The Beast in the Basement", "A politician’s mansion conceals a monstrous secret in its basement. When a journalist sneaks in, they uncover horrors that go beyond corruption."),
                ("Deadly Visions", "A psychic sees visions of murders committed by undead assassins. As the victims increase, she realizes she may be next."),
                ("The Undying Witness", "A detective protects the sole witness of a high-profile murder, who happens to be a zombie struggling to control his violent instincts."),
                ("Echoes in the Crypt", "A group of spelunkers is trapped in a cave with a rising tide—and an ancient race of skeletal guardians closing in."),
                ("The Siren’s Song", "A deep-sea salvage diver becomes the target of a vengeful siren, whose haunting melody attracts both predators and the undead."),
                ("The Wendigo Pact", "A fugitive fleeing into the wilderness makes a deal with a wendigo to evade capture, but its price turns deadly."),
                ("Hollow Eyes", "A relentless killer leaves victims drained of life, and the only lead is a legend about a banshee haunting the city."),
                ("The Midnight Stalker", "A vampire serial killer targets the elites of a metropolitan city. An ex-cop must confront his own past to hunt the creature down."),
                ("The Gravekeeper’s Warning", "A mortician discovers a corpse that refuses to stay dead, and its presence hints at a larger conspiracy involving the undead."),
                ("The Forsaken Pact", "A secret society uses an ancient spell to raise the dead as enforcers, but when their magic spirals out of control, a rogue member fights to stop them."),
                ("Into the Maw", "An investigative journalist infiltrates a fishing village where every outsider vanishes, discovering the locals serve a monstrous sea deity."),
                ("The Vampire’s Mark", "A government agent uncovers a vampire syndicate using corporate fronts to smuggle human blood. When he’s bitten, he must destroy them before succumbing."),
                ("The Bone Collector", "A serial killer leaves behind bone sculptures at each crime scene, and the detective on the case discovers they’re being assembled for a monstrous ritual."),
                ("The Monster Next Door", "A woman suspects her reclusive neighbor is hiding something horrifying. Her investigation reveals he’s a ghoul feeding on the town’s homeless."),
                ("The Shadow Pact", "A young lawyer defends a man accused of being a werewolf, only to uncover a pack operating in the city and silencing those who expose them."),
                ("The Undead Conspiracy", "A whistleblower goes on the run after discovering her pharmaceutical company is testing a serum that turns the dead into weapons."),
                ("The Altar of Fear", "A cult performs rituals that summon undead creatures to do their bidding. A journalist trying to expose them becomes their next target."),
                ("Ashes of the Forgotten", "A firefighter battling a series of mysterious blazes discovers that the fires are caused by vengeful spirits from a tragic event."),
                ("The Lurking Fog", "A toxic fog engulfs a city, turning those who breathe it into undead monsters. A group of survivors must escape before they join the horde.")
            };

            // Add Titles and Descriptions to the Dictionary
            bank.Add(GenreType.Thriller, titleDescPairs);
        }
    }
}
